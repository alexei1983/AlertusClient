namespace Llc.GoodConsulting.Web.ThirdParty.Alertus.Model
{
    public class AlertusContactMethod : IAlertusEntity
    {
        public string? Name { get; set; }
        public int? Id { get; set; }
        public string? Description { get; set; }
        public AlertusContactMethodType? ContactMethodType { get; set; }
    }
}