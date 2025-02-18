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
    public class AlertusVersion(AlertusClient client)
    {
        readonly AlertusClient client = client;

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Version string.</returns>
        public async Task<string?> Get()
        {
            var response = await client.Get(AlertusConstants.VersionUri);
            response.ExpectSuccess();
            return await response.AsStringAsync();
        }
    }
}