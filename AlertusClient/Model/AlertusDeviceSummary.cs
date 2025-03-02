namespace Llc.GoodConsulting.Web.ThirdParty.Alertus.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class AlertusDeviceSummary : IAlertusEntity
    {
        public List<AlertusDeviceSummaryDetail> DeviceTypes { get; set; } = [];
    }
}