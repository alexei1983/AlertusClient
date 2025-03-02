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
    public class AlertusActivation(AlertusClient client)
    {
        readonly AlertusClient client = client;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <exception cref="ArgumentException"></exception>
        public async Task LaunchEmergency(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("Alertus emergency activation text is required.", nameof(text));
            var response = await client.PostJson(new AlertusEmergencyActivation() { Text = text }, 
                                                $"{AlertusConstants.ActivationUri}/emergency");
            response.ExpectSuccess();
        }
    }
}