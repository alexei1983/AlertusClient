
namespace Llc.GoodConsulting.Web.ThirdParty.Alertus.Model
{
    public class AlertusCustomField : IAlertusEntity
    {
        public int? Id { get; set; }
        public string? Field { get; set; }
        public string? Value { get; set; }
    }
}
