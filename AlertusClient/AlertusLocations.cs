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
    public class AlertusLocations(AlertusClient client)
    {
        readonly AlertusClient client = client;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contact"></param>
        /// <returns>True if the Alertus location exists and was updated, else false.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<bool> CreateOrUpdate(AlertusLocation location)
        {
            if (location is null)
                throw new ArgumentNullException(nameof(location), "Alertus location is required.");
            var response = await client.PutJson(location, AlertusConstants.LocationsUri);
            response.ExpectSuccess();
            var responseStr = await response.AsStringAsync();
            return AlertusConstants.True.Equals(responseStr, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<AlertusLocation>> List()
        {
            var result = await client.Get($"{AlertusConstants.LocationsUri}/details");
            result.ExpectSuccess();
            return await result.AsJsonEntitiesAsync<AlertusLocation>(client.JsonOptions);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task Delete(int locationId)
        {
            if (locationId < 1)
                throw new ArgumentException("Invalid Alertus location ID.", nameof(locationId));

            var response = await client.Delete($"{AlertusConstants.LocationsUri}/{locationId}");
            response.ExpectSuccess();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task Delete(AlertusLocation location)
        {
            if (location is null || !location.Id.HasValue)
                throw new ArgumentException("Invalid Alertus location.", nameof(location));

            await Delete(location.Id.Value);
        }
    }
}