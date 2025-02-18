
namespace Llc.GoodConsulting.Web.ThirdParty.Alertus.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class AlertusContact : IAlertusEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Id { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? LanguageId { get; set; }
        public string? JobTitle { get; set; }
        public string? CountryId { get; set; }
        public string? Location { get; set; }
        public List<AlertusContactMethodDetail> ContactMethodDetails { get; set; } = [];
    }
}
