namespace Llc.GoodConsulting.Web.ThirdParty.Alertus.Model
{
    public class AlertusContactMethodDetail : IAlertusEntity
    {
        public string? Value { get; set; }
        public int? Id { get; set; }
        public AlertusContactMethod? ContactMethod { get; set; }
    }
}