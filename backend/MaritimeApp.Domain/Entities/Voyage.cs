namespace MaritimeApp.Domain.Entities;

public class Voyage
{
    public int Id { get; set; }
    public int ShipId { get; set; }

    public DateTime VoyageDate { get; set; }
    public int DeparturePortId { get; set; }
    public int ArrivalPortId { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }

    public Ship Ship { get; set; } = null!;
    public Port DeparturePort { get; set; } = null!;
    public Port ArrivalPort { get; set; } = null!;
}
