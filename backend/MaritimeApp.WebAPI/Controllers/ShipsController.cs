using MaritimeApp.Application.DTOs;
using MaritimeApp.Domain.Entities;
using MaritimeApp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MaritimeApp.Infrastructure.Data;

namespace MaritimeApp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShipsController : ControllerBase
{
    private readonly MaritimeDbContext _dbContext;

    public ShipsController(MaritimeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShipDisplayDto>>> GetAll()
    {
        var ships = await _dbContext.Ships
            .Include(s => s.Specification)
            .ToListAsync();

        // Console.WriteLine($"Ship found: {ships}");
        foreach (var ship in ships)
        {
            Console.WriteLine($"Ship found: {ship}");
        }
        var result = ships.Select(s => new ShipDisplayDto
        {
            Id = s.Id,
            Name = s.Name,
            MaxSpeed = s.Specification?.MaxSpeedKnots,
            ImoNumber = s.ImoNumber
        });

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ShipFullDataDto>> GetById(int id)
    {
        var ship = await _dbContext.Ships
            .Include(s => s.ShipType)
            .Include(s => s.FlagCountry)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (ship == null)
            return NotFound();

        var specification = await _dbContext.ShipSpecifications
            .FirstOrDefaultAsync(spec => spec.ShipId == id);

        var companyRelations = await _dbContext.ShipCompanyRelations
            .Include(r => r.Company)
            .Where(r => r.ShipId == id)
            .ToListAsync();

        var lastLocation = await _dbContext.ShipLocationLogs
            .Where(l => l.ShipId == id)
            .OrderByDescending(l => l.RecordedAt)
            .FirstOrDefaultAsync();

        var owner = companyRelations.FirstOrDefault(r => r.Role == "Owner")?.Company?.Name;
        var operatorName = companyRelations.FirstOrDefault(r => r.Role == "Operator")?.Company?.Name;

       Console.WriteLine($"Ship found: {ship.Name} (ID: {ship.Id}) and ImoNumber: {ship.ImoNumber}");

        if (specification != null)
        {
            Console.WriteLine("Ship specification:");
            foreach (var prop in specification.GetType().GetProperties())
            {
                var name = prop.Name;
                var value = prop.GetValue(specification, null);
                Console.WriteLine($"  {name}: {value}");
            }
        }
        else
        {
            Console.WriteLine("No ship specification found.");
        }

        Console.WriteLine($"Ship company relations found: {string.Join(", ", companyRelations.Select(r => $"{r.Role}: {r.Company.Name}"))}");

        if (lastLocation != null)
        {
            Console.WriteLine($"Ship last location found:");
            Console.WriteLine($"  Latitude: {lastLocation.Latitude}");
            Console.WriteLine($"  Longitude: {lastLocation.Longitude}");
            Console.WriteLine($"  Speed: {lastLocation.SpeedKnots} knots");
            Console.WriteLine($"  Time: {lastLocation.RecordedAt}");
        }
        else
        {
            Console.WriteLine("No ship location logs found.");
        }

        return Ok(new ShipFullDataDto
        {
            Id = ship.Id,
            Name = ship.Name,
            ImoNumber = ship.ImoNumber,
            ShipType = ship.ShipType?.TypeName,
            FlagCountry = ship.FlagCountry?.Name,
            Owner = owner,
            Operator = operatorName,
            YearBuilt = specification?.YearBuilt,
            MaxSpeed = specification?.MaxSpeedKnots,
            GrossTonnage = specification?.GrossTonnage,
            Deadweight = specification?.Deadweight,
            LengthMeters = specification?.LengthMeters,
            BeamMeters = specification?.BeamMeters,
            DraftMeters = specification?.DraftMeters,
            LastSeenAt = lastLocation?.RecordedAt,
            LastKnownLat = lastLocation?.Latitude ?? null,
            LastKnownLon = lastLocation?.Longitude ?? null
        });
    }



    [HttpPost]
    public async Task<ActionResult<ShipFullDataDto>> Create(ShipFullDataDto dto)
    {
        var ship = new Ship
        {
            Name = dto.Name,
            ImoNumber = dto.ImoNumber.ToString(),
            Specification = new ShipSpecification
            {
                MaxSpeedKnots = dto.MaxSpeed
            }
        };

        await _dbContext.Ships.AddAsync(ship);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = ship.Id }, dto);
    }

    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ShipFullDataDto dto)
    {
        var ship = await _dbContext.Ships
            .Include(s => s.ShipType)
            .Include(s => s.FlagCountry)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (ship == null)
            return NotFound();

        // 1. Update basic Ship fields
        ship.Name = dto.Name;
        ship.ImoNumber = dto.ImoNumber;

        // Optional: update ShipType via lookup
        if (!string.IsNullOrWhiteSpace(dto.ShipType))
        {
            var type = await _dbContext.ShipTypes.FirstOrDefaultAsync(t => t.TypeName == dto.ShipType);
            if (type != null)
                ship.ShipTypeId = type.Id;
        }

        // Optional: update FlagCountry via lookup
        if (!string.IsNullOrWhiteSpace(dto.FlagCountry))
        {
            var country = await _dbContext.Countries.FirstOrDefaultAsync(c => c.Name == dto.FlagCountry);
            if (country != null)
                ship.FlagCountryId = country.Id;
        }

        // 2. Update or create Specification
        var spec = await _dbContext.ShipSpecifications.FirstOrDefaultAsync(s => s.ShipId == id);
        if (spec == null)
        {
            spec = new ShipSpecification { ShipId = ship.Id };
            _dbContext.ShipSpecifications.Add(spec);
        }
        spec.YearBuilt = dto.YearBuilt;
        spec.MaxSpeedKnots = dto.MaxSpeed;
        spec.GrossTonnage =(int) dto.GrossTonnage;
        spec.Deadweight = (int)dto.Deadweight;
        spec.LengthMeters = dto.LengthMeters;
        spec.BeamMeters = dto.BeamMeters;
        spec.DraftMeters = dto.DraftMeters;

        // 3. Update or insert Owner/Operator relationships
        var companyRelations = await _dbContext.ShipCompanyRelations
            .Where(r => r.ShipId == id)
            .ToListAsync();

        foreach (var role in new[] { "Owner", "Operator" })
        {
            var companyName = role == "Owner" ? dto.Owner : dto.Operator;
            if (!string.IsNullOrWhiteSpace(companyName))
            {
                var company = await _dbContext.Companies.FirstOrDefaultAsync(c => c.Name == companyName);
                if (company == null)
                {
                    company = new Company { Name = companyName };
                    _dbContext.Companies.Add(company);
                    await _dbContext.SaveChangesAsync(); // Ensure ID is set
                }

                var relation = companyRelations.FirstOrDefault(r => r.Role == role);
                if (relation == null)
                {
                    _dbContext.ShipCompanyRelations.Add(new ShipCompanyRelation
                    {
                        ShipId = ship.Id,
                        CompanyId = company.Id,
                        Role = role
                    });
                }
                else
                {
                    relation.CompanyId = company.Id; // update existing
                }
            }
        }

    // 4. Add new location log (append-only)
    if (dto.LastSeenAt.HasValue && dto.LastKnownLat.HasValue && dto.LastKnownLon.HasValue)
    {
        var locationLog = new ShipLocationLog
        {
            ShipId = ship.Id,
            RecordedAt = DateTime.SpecifyKind(dto.LastSeenAt.Value, DateTimeKind.Utc),
            Latitude = dto.LastKnownLat.Value,
            Longitude = dto.LastKnownLon.Value,
            SpeedKnots = dto.MaxSpeed
        };
        _dbContext.ShipLocationLogs.Add(locationLog);
    }

    await _dbContext.SaveChangesAsync();
    return NoContent();
}

    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        Console.WriteLine($"Deleting ship with ID: {id}");
        var ship = await _dbContext.Ships
            .Include(s => s.Specification)
            .Include(s => s.Voyages)
            .Include(s => s.LocationLogs)
            .Include(s => s.VisitedCountries)
            .Include(s => s.CompanyRelations)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (ship == null)
            return NotFound();

        _dbContext.Ships.Remove(ship);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }


    [HttpGet("{id:int}/voyages")]
    public async Task<ActionResult<IEnumerable<VoyageDto>>> GetVoyagesForShip(int id)
    {
    
        bool exists = await _dbContext.Ships.AnyAsync(s => s.Id == id);
        if (!exists)
            return NotFound($"Ship {id} not found.");

        var voyages = await _dbContext.Voyages
            .Where(v => v.ShipId == id)
            .Include(v => v.DeparturePort).ThenInclude(p => p.Country)  
            .Include(v => v.ArrivalPort).ThenInclude(p => p.Country)
            .AsNoTracking()
            .OrderByDescending(v => v.StartTime ?? v.VoyageDate)  
            .ToListAsync();

        var result = voyages.Select(v => new VoyageDto(
            v.Id,
            v.StartTime,
            v.DeparturePort?.Name ?? "—",
            v.DeparturePort?.Country?.Name ?? "—",
            v.EndTime,
            v.ArrivalPort?.Name ?? "—",
            v.ArrivalPort?.Country?.Name ?? "—"
        ));


        return Ok(result);
    }

}
