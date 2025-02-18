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
    public class AlertusSystemErrors(AlertusClient client)
    {
        readonly AlertusClient client = client;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task CreateIntegrationError(AlertusSystemError error)
        {
            if (error is null)
                throw new ArgumentNullException(nameof(error), "Alertus system integration error is required.");
            var response = await client.PostJson(error, $"{AlertusConstants.SystemErrorsUri}/integration");
            response.ExpectSuccess();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<AlertusSystemError>> List()
        {
            var result = await client.Get(AlertusConstants.SystemErrorsUri);
            result.ExpectSuccess();
            return await result.AsJsonEntitiesAsync<AlertusSystemError>(client.JsonOptions);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task DeleteIntegrationErrors(string? subGroup = null)
        {
            var response = await client.Delete($"{AlertusConstants.SystemErrorsUri}/integration{(!string.IsNullOrEmpty(subGroup) ? $"?subGroup={Uri.EscapeDataString(subGroup)}" : string.Empty)}");
            response.ExpectSuccess();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task Delete(string errorId)
        {
            if (string.IsNullOrEmpty(errorId))
                throw new ArgumentException("Invalid Alertus system error ID.", nameof(errorId));

            var response = await client.Delete($"{AlertusConstants.SystemErrorsUri}/{errorId}");
            response.ExpectSuccess();
        }
    }
}