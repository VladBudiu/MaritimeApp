namespace MaritimeApp.Domain.Entities;

public class ShipSpecification
{
    public int Id { get; set; }
    public int ShipId { get; set; }

    public int? YearBuilt { get; set; }
    public float? MaxSpeedKnots { get; set; }
    public int? GrossTonnage { get; set; }
    public int? Deadweight { get; set; }
    public float? LengthMeters { get; set; }
    public float? BeamMeters { get; set; }
    public float? DraftMeters { get; set; }

    public Ship Ship { get; set; } = null!;
}
