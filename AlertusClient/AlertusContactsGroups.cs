using Llc.GoodConsulting.Web.ThirdParty.Alertus.Model;
using Llc.GoodConsulting.Web.EnhancedWebRequest;

namespace Llc.GoodConsulting.Web.ThirdParty.Alertus
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <param name="client"></param>
    public class AlertusContactsGroups(AlertusClient client)
    {
        readonly AlertusClient client = client;

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>This method will add new contacts to the group in Alertus and delete missing ones.</remarks>
        /// <param name="contact"></param>
        /// <returns>True if the contact was created, else false.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task Reconcile(int groupId, params string[] contactIds)
        {
            if (groupId < 1)
                throw new ArgumentException("Invalid Alertus group ID.", nameof(groupId));

            if (contactIds is null || contactIds.Length < 1)
                return;

            var response = await client.PutJson<List<string>>([..contactIds], $"{AlertusConstants.ContactsGroupsUri}/{groupId}");
            response.ExpectSuccess();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<AlertusContactGroupMember>> List(int groupId)
        {
            if (groupId < 1)
                throw new ArgumentException("Invalid Alertus group ID.", nameof(groupId));

            var result = await client.Get(new Dictionary<string, string>() { { "ids", $"{groupId}" } }, 
                                          AlertusConstants.ContactsGroupsUri);
            result.ExpectSuccess();
            return await result.AsJsonEntitiesAsync<AlertusContactGroupMember>(client.JsonOptions);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="contactIds"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task Remove(int groupId, params string[] contactIds)
        {
            if (groupId < 1)
                throw new ArgumentException("Invalid Alertus group ID.", nameof(groupId));

            if (contactIds is null || contactIds.Length < 1)
                return;

            var contactsInGroup = await List(groupId);

            var newContactsInGroup = contactsInGroup.Where(c => !string.IsNullOrEmpty(c.ContactId) &&
                                                                !contactIds.Any(ci => ci.Equals(c.ContactId)))
                                                    .Select(c => c.ContactId);

            await Reconcile(groupId, [..newContactsInGroup]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="contactIds"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task Add(int groupId, params string[] contactIds)
        {
            if (groupId < 1)
                throw new ArgumentException("Invalid Alertus group ID.", nameof(groupId));

            if (contactIds is null || contactIds.Length < 1)
                return;

            var contactsInGroup = await List(groupId);

            var newContactsInGroup = contactsInGroup.Where(c => !string.IsNullOrEmpty(c.ContactId))
                                                    .Select(c => c.ContactId)
                                                    .Union(contactIds);

            await Reconcile(groupId, [.. newContactsInGroup]);
        }
    }
}