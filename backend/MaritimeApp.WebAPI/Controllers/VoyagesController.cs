using MaritimeApp.Application.DTOs;
using MaritimeApp.Domain.Entities;
using MaritimeApp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MaritimeApp.Infrastructure.Data;

namespace MaritimeApp.WebAPI.Controllers;
[Route("api/[controller]")]
public class VoyagesController : ControllerBase
{
    private readonly MaritimeDbContext _db;

    public VoyagesController(MaritimeDbContext db) => _db = db;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateVoyageDto dto)
    {
        var departure = await _db.Ports.FirstOrDefaultAsync(p => p.Name == dto.DeparturePort);
        var arrival = await _db.Ports.FirstOrDefaultAsync(p => p.Name == dto.ArrivalPort);

        if (departure == null || arrival == null)
            return BadRequest("Invalid port names.");

        var voyage = new Voyage
        {
            ShipId = dto.ShipId,
            DeparturePortId = departure.Id,
            ArrivalPortId = arrival.Id,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            VoyageDate = dto.StartTime?.Date ?? DateTime.Today
        };


        await _db.Voyages.AddAsync(voyage);
        await _db.SaveChangesAsync();

        return Ok(new
            {
                voyage.Id,
                dto.ShipId,
                dto.DeparturePort,
                dto.ArrivalPort,
                dto.StartTime,
                dto.EndTime
            });

    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var voyage = await _db.Voyages.FindAsync(id);
        if (voyage == null)
            return NotFound();

        _db.Voyages.Remove(voyage);
        await _db.SaveChangesAsync();

        return NoContent();
}

}
