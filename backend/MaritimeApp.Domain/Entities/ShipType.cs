namespace MaritimeApp.Domain.Entities;

public class ShipType
{
    public int Id { get; set; }
    public string TypeName { get; set; } = null!;

    public ICollection<Ship> Ships { get; set; } = new List<Ship>();
}
