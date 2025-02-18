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
    public class AlertusContacts(AlertusClient client)
    {
        readonly AlertusClient client = client;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contact"></param>
        /// <returns>True if the contact was created, else false if it was updated.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<bool> CreateOrUpdate(AlertusContact contact)
        {
            if (contact is null)
                throw new ArgumentNullException(nameof(contact), "Alertus contact is required.");
            var response = await client.PutJson(contact, AlertusConstants.ContactsUri);
            response.ExpectSuccess();
            var responseStr = await response.AsStringAsync();
            return AlertusConstants.False.Equals(responseStr, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<AlertusContact?> Get(string contactId)
        {
            if (string.IsNullOrEmpty(contactId))
                throw new ArgumentException("Invalid Alertus contact ID.", nameof(contactId));
            var result = await client.Get($"{AlertusConstants.ContactsUri}/{contactId}");
            result.ExpectSuccess();
            return await result.AsJsonEntityAsync<AlertusContact>(client.JsonOptions);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactId"></param>
        /// <param name="contactMethod"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<bool> CreateOrUpdateContactMethod(string contactId, AlertusContactMethodDetail contactMethod)
        {
            if (string.IsNullOrEmpty(contactId))
                throw new ArgumentException("Invalid Alertus contact ID.", nameof(contactId));

            if (contactMethod.ContactMethod?.Id is null)
                throw new ArgumentException("Invalid Alertus contact method.", nameof(contactMethod));

            var contact = await Get(contactId);

            if (contact is null || string.IsNullOrEmpty(contact.Id))
                throw new Exception($"Alertus contact with ID {contactId} was not found.");

            var method = contact.ContactMethodDetails.FirstOrDefault(cm => cm.ContactMethod?.Id == contactMethod?.ContactMethod?.Id);

            if (method is null)
                contact.ContactMethodDetails.Add(contactMethod);
            else
            {
                contact.ContactMethodDetails.Remove(method);
                contact.ContactMethodDetails.Add(contactMethod);
            }
            return await CreateOrUpdate(contact);
        }
    }
}