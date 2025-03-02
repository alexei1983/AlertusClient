namespace Llc.GoodConsulting.Web.ThirdParty.Alertus.Model
{
    /// <summary>
    /// Represents a device in the Alertus platform.
    /// </summary>
    public class AlertusDevice : IAlertusEntity
    {
        /// <summary>
        /// Device ID.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Name of the device.
        /// </summary>
        public string? Name { get; set; }
    }
}