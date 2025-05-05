namespace MaritimeApp.Application.DTOs;

public class ShipDisplayDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public float? MaxSpeed { get; set; }
    public string ImoNumber { get; set; }
}


public class ShipFullDataDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string ImoNumber { get; set; }
    public string? ShipType { get; set; }
    public string? FlagCountry { get; set; }
    public string? Owner { get; set; }
    public string? Operator { get; set; }
    public int? YearBuilt { get; set; }
    public float? MaxSpeed { get; set; }
    public float? GrossTonnage { get; set; }
    public float? Deadweight { get; set; }
    public float? LengthMeters { get; set; }
    public float? BeamMeters { get; set; }
    public float? DraftMeters { get; set; }
    public DateTime? LastSeenAt { get; set; }
    public float? LastKnownLat { get; set; }
    public float? LastKnownLon { get; set; }
}


public class ShipCreateDto
{
    public string Name { get; set; } = null!;
    public string? ImoNumber { get; set; }
    public string? ShipType { get; set; }
    public string? FlagCountry { get; set; }
    public string? Owner { get; set; }
    public string? Operator { get; set; }
    public int? YearBuilt { get; set; }
    public float? MaxSpeed { get; set; }
    public float? GrossTonnage { get; set; }
    public float? Deadweight { get; set; }
    public float? LengthMeters { get; set; }
    public float? BeamMeters { get; set; }
    public float? DraftMeters { get; set; }
}


public class ShipSummaryDto
{
    public ShipSummaryDto(int id, string name, string? imoNumber)
    {
        Id = id;
        Name = name;
        ImoNumber = imoNumber;
    }

    
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string ImoNumber { get; set; } = null!;
}

public class ShipVisitSummaryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? ImoNumber { get; set; }
    public DateTime? LastArrival { get; set; }
    public DateTime? LastDeparture { get; set; }

    public ShipVisitSummaryDto(int id, string name, string? imo, DateTime? lastArrival, DateTime? lastDeparture)
    {
        Id = id;
        Name = name;
        ImoNumber = imo;
        LastArrival = lastArrival;
        LastDeparture = lastDeparture;
    }
}