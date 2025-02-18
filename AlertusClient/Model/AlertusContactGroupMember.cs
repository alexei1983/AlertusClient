
namespace Llc.GoodConsulting.Web.ThirdParty.Alertus.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class AlertusContactGroupMember : IAlertusEntity
    {
        public int? Id { get; set; }
        public int? GroupId { get; set; }
        public string? GroupName { get; set; }
        public string? ContactId { get; set; }
    }
}
