using MaritimeApp.Application.DTOs.Port;
using MaritimeApp.Domain.Entities;
using MaritimeApp.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MaritimeApp.Application.DTOs;


namespace MaritimeApp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PortsController : ControllerBase
{
    private readonly MaritimeDbContext _dbContext;

    public PortsController(MaritimeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /* ────────────────────────────────
       GET  /api/ports
       overview list (Id, Name, Country, VoyagesCount)
    ──────────────────────────────────*/
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PortDto>>> GetAll()
    {
        var ports = await _dbContext.Ports
            .Include(p => p.Country)
            .Include(p => p.DepartingVoyages)
            .Include(p => p.ArrivingVoyages)
            .AsNoTracking()
            .ToListAsync();

        var result = ports.Select(p => new PortDto(
            p.Id,
            p.Name,
            p.Country?.Name,
            p.DepartingVoyages.Count + p.ArrivingVoyages.Count));

        return Ok(result);
    }

    /* ────────────────────────────────
       GET  /api/ports/{id}
       full details + list of ships that ever visited
    ──────────────────────────────────*/
    [HttpGet("{id:int}")]
    public async Task<ActionResult<object>> GetById(int id)
    {
        var port = await _dbContext.Ports
            .Include(p => p.Country)
            .Include(p => p.DepartingVoyages).ThenInclude(v => v.Ship)
            .Include(p => p.ArrivingVoyages).ThenInclude(v => v.Ship)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        if (port is null)
            return NotFound();

        var shipVoyages = port.DepartingVoyages
            .Concat(port.ArrivingVoyages)
            .GroupBy(v => v.Ship)
            .Select(g => new ShipVisitSummaryDto(
                g.Key.Id,
                g.Key.Name,
                g.Key.ImoNumber,
                g.Where(v => v.ArrivalPortId == port.Id).Max(v => v.EndTime),
                g.Where(v => v.DeparturePortId == port.Id).Max(v => v.StartTime)
            ))
            .ToList();

        var dto = new
        {
            Id = port.Id,
            Name = port.Name,
            CountryName = port.Country?.Name,
            VoyagesCount = port.DepartingVoyages.Count + port.ArrivingVoyages.Count,
            ShipsVisited = shipVoyages
        };

        return Ok(dto);
    }


    /* ────────────────────────────────
       GET  /api/ports/{id}/ships
       (if Angular uses separate call)
    ──────────────────────────────────*/
    [HttpGet("{id:int}/ships")]
    public async Task<ActionResult<IEnumerable<ShipSummaryDto>>> GetShips(int id)
    {
        var ships = await _dbContext.Voyages
            .Where(v => v.DeparturePortId == id || v.ArrivalPortId == id)
            .Include(v => v.Ship)
            .Select(v => v.Ship)
            .Distinct()
            .AsNoTracking()
            .ToListAsync();

        var result = ships.Select(s => new ShipSummaryDto(s.Id, s.Name, s.ImoNumber));
        return Ok(result);
    }

    /* ────────────────────────────────
       POST /api/ports
       body: { "name": "...", "countryName": "..." }
    ──────────────────────────────────*/
    [HttpPost]
    public async Task<ActionResult<PortDto>> Create(CreatePortDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            return BadRequest("Name is required.");

        // ensure Country row exists (or allow null)
        Country? country = null;
        if (!string.IsNullOrWhiteSpace(dto.CountryName))
        {
            country = await _dbContext.Countries
                .FirstOrDefaultAsync(c => c.Name == dto.CountryName);

            if (country is null)
            {
                country = new Country { Name = dto.CountryName! };
                _dbContext.Countries.Add(country);
                await _dbContext.SaveChangesAsync(); // get ID
            }
        }

        var port = new Port
        {
            Name       = dto.Name,
            CountryId  = country?.Id ?? 0,   // 0 if unknown / not supplied
            Country    = country
        };

        await _dbContext.Ports.AddAsync(port);
        await _dbContext.SaveChangesAsync();

        var result = new PortDto(
            port.Id,
            port.Name,
            country?.Name,
            0);

        return CreatedAtAction(nameof(GetById), new { id = port.Id }, result);
    }

    /* ────────────────────────────────
       DELETE /api/ports/{id}
       (optional, mirrors ShipsController style)
    ──────────────────────────────────*/
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var port = await _dbContext.Ports.FindAsync(id);
        if (port is null)
            return NotFound();

        _dbContext.Ports.Remove(port);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }
}
