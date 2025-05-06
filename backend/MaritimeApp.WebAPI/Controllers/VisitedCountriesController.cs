using MaritimeApp.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MaritimeApp.Application.DTOs;

namespace MaritimeApp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VisitedCountriesController : ControllerBase
{
    private readonly MaritimeDbContext _db;

    public VisitedCountriesController(MaritimeDbContext db)
    {
        _db = db;
    }

    [HttpGet("lastyear/{shipId:int}")]
    public async Task<ActionResult<IEnumerable<object>>> GetVisitedCountriesInLastYear(int shipId)
    {
        var oneYearAgo = DateTime.UtcNow.AddYears(-1);

        var voyages = await _db.Voyages
            .Where(v => v.ShipId == shipId && v.StartTime >= oneYearAgo)
            .Include(v => v.DeparturePort).ThenInclude(p => p.Country)
            .Include(v => v.ArrivalPort).ThenInclude(p => p.Country)
            .ToListAsync();

        var allCountries = voyages
            .SelectMany(v => new[]
            {
                
                v.ArrivalPort.Country?.Name
            })
            .Where(c => !string.IsNullOrWhiteSpace(c));

        var result = allCountries
            .GroupBy(name => name!)
            .OrderByDescending(g => g.Count())
            .Select(g => new
            {
                countryName = g.Key,
                visitCount = g.Count()
            })
            .ToList();

        return Ok(result);

    }



}
