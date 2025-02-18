
namespace Llc.GoodConsulting.Web.ThirdParty.Alertus.Model
{
    public class AlertusSystemError : IAlertusEntity
    {
        public string? Id { get; set; }
        public string? ShortDescription { get; set; }
        public string? ActionToTake { get; set; }
        public string? LongDescription { get; set; }
        public string? DebugDetails { get; set; }
        public string? SubGroup { get; set; }
        public int? OccurrenceCount { get; set; }
        public long? FirstOccurrence { get; set; }
        public long? LastOccurrence { get; set; }
        public bool? NotifyOrganizationsTechnical { get; set; }
        public bool? DisplayAlertToUser { get; set; }
        public bool? NotifyAlertusSupport { get; set; }

    }
}
