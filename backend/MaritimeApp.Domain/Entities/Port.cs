namespace MaritimeApp.Domain.Entities;

public class Port
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int CountryId { get; set; }

    public Country Country { get; set; } = null!;
    public ICollection<Voyage> DepartingVoyages { get; set; } = new List<Voyage>();
    public ICollection<Voyage> ArrivingVoyages { get; set; } = new List<Voyage>();
}
