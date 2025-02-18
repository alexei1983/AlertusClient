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
    public class AlertusContactMethods(AlertusClient client)
    {
        readonly AlertusClient client = client;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactMethod"></param>
        /// <returns><see cref="AlertusContactMethod"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<AlertusContactMethod?> Create(AlertusContactMethod contactMethod)
        {
            if (contactMethod is null)
                throw new ArgumentNullException(nameof(contactMethod), "Alertus contact method is required.");
            var response = await client.PostJson(contactMethod, AlertusConstants.ContactMethodsUri);
            response.ExpectSuccess();
            return await response.AsJsonEntityAsync<AlertusContactMethod>(client.JsonOptions);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactMethod"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> Update(AlertusContactMethod contactMethod)
        {
            if (contactMethod is null)
                throw new ArgumentNullException(nameof(contactMethod), "Alertus contact method is required.");

            if (!contactMethod.Id.HasValue)
                throw new ArgumentException("Cannot update entity: Alertus contact method ID is required.", nameof(contactMethod));

            var response = await client.PutJson(contactMethod, AlertusConstants.ContactMethodsUri);
            response.ExpectSuccess();
            var responseStr = await response.AsStringAsync();
            return "true".Equals(responseStr, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactMethodId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<AlertusContactMethod?> Get(int contactMethodId)
        {
            if (contactMethodId < 1)
                throw new ArgumentException("Invalid Alertus contact method ID.", nameof(contactMethodId));
            var result = await client.Get($"{AlertusConstants.ContactMethodsUri}/{contactMethodId}");
            result.ExpectSuccess();
            return await result.AsJsonEntityAsync<AlertusContactMethod>(client.JsonOptions);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<AlertusContactMethod>?> List()
        {
            var result = await client.Get($"{AlertusConstants.ContactMethodsUri}/details");
            result.ExpectSuccess();
            return await result.AsJsonEntitiesAsync<AlertusContactMethod>(client.JsonOptions);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<AlertusContactMethodType>?> ListTypes()
        {
            var result = await client.Get($"{AlertusConstants.ContactMethodTypesUri}");
            result.ExpectSuccess();
            return await result.AsJsonEntitiesAsync<AlertusContactMethodType>(client.JsonOptions);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactMethodId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task Delete(int contactMethodId)
        {
            if (contactMethodId < 1)
                throw new ArgumentException("Invalid Alertus contact method ID.", nameof(contactMethodId));

            var response = await client.Delete($"{AlertusConstants.ContactMethodsUri}/{contactMethodId}");
            response.ExpectSuccess();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactMethod"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task Delete(AlertusContactMethod contactMethod)
        {
            if (contactMethod is null || !contactMethod.Id.HasValue)
                throw new ArgumentException("Invalid Alertus contact method.", nameof(contactMethod));

            await Delete(contactMethod.Id.Value);
        }
    }
}