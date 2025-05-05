namespace MaritimeApp.Application.DTOs;

public record VoyageDto(
    int     Id,
    DateTime? StartTime,      
    string  DeparturePort,
    string DepartureCountry, 
    DateTime? EndTime,        
    string  ArrivalPort,
    string ArrivalCountry
);


public record CreateVoyageDto
{
    public int ShipId { get; set; }
    public string DeparturePort { get; set; } = null!;
    public string ArrivalPort { get; set; } = null!;
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
}
