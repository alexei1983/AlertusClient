namespace Llc.GoodConsulting.Web.ThirdParty.Alertus.Model
{
    public class AlertusDeviceSummaryDetail : IAlertusEntity
    {
        public int? Count { get; set; }
        public int? DeviceTypeId { get; set; }
        public string? DeviceType { get; set; }
    }
}