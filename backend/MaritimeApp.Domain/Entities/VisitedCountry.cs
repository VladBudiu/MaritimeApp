namespace MaritimeApp.Domain.Entities;

public class VisitedCountry
{
    public int Id { get; set; }

    public int ShipId { get; set; }
    public int CountryId { get; set; }
    public DateTime VisitedOn { get; set; }

    public Ship Ship { get; set; } = null!;
    public Country Country { get; set; } = null!;
}
