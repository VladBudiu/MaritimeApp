namespace MaritimeApp.Domain.Entities;

public class Ship
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string ImoNumber { get; set; } = null!;
    public int? ShipTypeId { get; set; }
    public int? FlagCountryId { get; set; }

    public ShipType? ShipType { get; set; }
    public Country? FlagCountry { get; set; }

    public ShipSpecification? Specification { get; set; }
    public ICollection<ShipCompanyRelation> CompanyRelations { get; set; } = new List<ShipCompanyRelation>();
    public ICollection<ShipLocationLog> LocationLogs { get; set; } = new List<ShipLocationLog>();
    public ICollection<VisitedCountry> VisitedCountries { get; set; } = new List<VisitedCountry>();
    public ICollection<Voyage> Voyages { get; set; } = new List<Voyage>();
}
