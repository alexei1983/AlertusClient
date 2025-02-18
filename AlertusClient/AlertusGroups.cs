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
    public class AlertusGroups(AlertusClient client)
    {
        readonly AlertusClient client = client;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contact"></param>
        /// <returns>True if the contact was created, else false.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<AlertusGroup?> Create(AlertusGroup group)
        {
            if (group is null)
                throw new ArgumentNullException(nameof(group), "Alertus group is required.");
            var response = await client.PostJson(group, AlertusConstants.GroupsUri);
            response.ExpectSuccess();
            return await response.AsJsonEntityAsync<AlertusGroup>(client.JsonOptions);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task Update(AlertusGroup group)
        {
            if (group is null)
                throw new ArgumentNullException(nameof(group), "Alertus group is required.");

            if (!group.Id.HasValue)
                throw new ArgumentException("Invalid Alertus group.", nameof(group));

            var response = await client.PutJson(group, AlertusConstants.GroupsUri);
            response.ExpectSuccess();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<AlertusGroup?> Get(int groupId)
        {
            if (groupId < 1)
                throw new ArgumentException("Invalid Alertus group ID.", nameof(groupId));
            var result = await client.Get($"{AlertusConstants.GroupsUri}/{groupId}");
            result.ExpectSuccess();
            return await result.AsJsonEntityAsync<AlertusGroup>(client.JsonOptions);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<AlertusGroup>?> List()
        {
            var result = await client.Get(AlertusConstants.GroupsUri);
            result.ExpectSuccess();
            return await result.AsJsonEntitiesAsync<AlertusGroup>(client.JsonOptions);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task Delete(int groupId)
        {
            if (groupId < 1)
                throw new ArgumentException("Invalid Alertus group ID.", nameof(groupId));

            var response = await client.Delete($"{AlertusConstants.GroupsUri}/{groupId}");
            response.ExpectSuccess();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task Delete(AlertusGroup group)
        {
            if (group is null || !group.Id.HasValue)
                throw new ArgumentException("Invalid Alertus group.", nameof(group));

            await Delete(group.Id.Value);
        }
    }
}