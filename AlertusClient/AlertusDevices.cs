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
    public class AlertusDevices(AlertusClient client)
    {
        readonly AlertusClient client = client;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contact"></param>
        /// <returns>True if the contact was created, else false.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<AlertusDeviceSummary?> ListSummary()
        {
            var response = await client.Get($"{AlertusConstants.AlertDevicesUri}/summary");
            response.ExpectSuccess();
            return await response.AsJsonEntityAsync<AlertusDeviceSummary>(client.JsonOptions);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<AlertusDevice>> List()
        {
            var response = await client.Get(AlertusConstants.AlertDevicesUri);
            response.ExpectSuccess();
            return await response.AsJsonEntitiesAsync<AlertusDevice>(client.JsonOptions);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<AlertusDeviceDetail>> ListDetails()
        {
            var response = await client.Get($"{AlertusConstants.AlertDevicesUri}/details");
            response.ExpectSuccess();
            return await response.AsJsonEntitiesAsync<AlertusDeviceDetail>(client.JsonOptions);
        }
    }
}