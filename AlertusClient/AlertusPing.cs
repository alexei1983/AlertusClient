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
    public class AlertusPing(AlertusClient client)
    {
        readonly AlertusClient client = client;

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Version string.</returns>
        public async Task<bool> Ping()
        {
            var response = await client.Get(AlertusConstants.PingUri);
            response.ExpectSuccess();
            var responseStr = await response.AsStringAsync();
            return AlertusConstants.Pong.Equals(responseStr, StringComparison.OrdinalIgnoreCase);
        }
    }
}