namespace MaritimeApp.Domain.Entities;

public class Country
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<Port> Ports { get; set; } = new List<Port>();
    public ICollection<Ship> FlaggedShips { get; set; } = new List<Ship>();
    public ICollection<VisitedCountry> VisitedCountries { get; set; } = new List<VisitedCountry>();
}
