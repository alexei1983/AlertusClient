namespace Llc.GoodConsulting.Web.ThirdParty.Alertus.Model
{
    /// <summary>
    /// Contains the details about a device in the Alertus platform.
    /// </summary>
    public class AlertusDeviceDetail : AlertusDevice, IAlertusEntity
    {
        public DateTime? LastVerifiedCheckinDate { get; set; }
        public string? Notes { get; set; }
        public int? Floor { get; set; }
        public int? DeviceTypeId { get; set; }
        public DateTime? LastCheckinDate { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public string? Footprint { get; set; }
    }
}