namespace MaritimeApp.Domain.Entities;

public class ShipLocationLog
{
    public int Id { get; set; }
    public int ShipId { get; set; }

    public float? Latitude { get; set; }
    public float? Longitude { get; set; }
    public float? SpeedKnots { get; set; }
    public DateTime RecordedAt { get; set; }

    public Ship Ship { get; set; } = null!;
}
