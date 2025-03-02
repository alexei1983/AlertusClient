
namespace Llc.GoodConsulting.Web.ThirdParty.Alertus.Model
{
    public class AlertusLocation : IAlertusEntity
    {
        public string? Name { get; set; }
        public int? Id { get; set; }
        public int? Floor { get; set; }
        public string? Description { get; set; }
        public string? State { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? CountryId { get; set; }
        public AlertusCoordinate? Coordinate { get; set; }
    }
}
