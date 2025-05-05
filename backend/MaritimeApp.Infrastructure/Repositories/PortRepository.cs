using MaritimeApp.Domain.Entities;
using MaritimeApp.Domain.Interfaces;
using MaritimeApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace  MaritimeApp.Infrastructure.Repositories;

public class PortRepository : Repository<Port>, IPortRepository
{

    private readonly MaritimeDbContext _context;
    public PortRepository(MaritimeDbContext context) : base(context) { }

     public async Task<IEnumerable<Ship>> GetShipsAtPortAsync(int portId)
    {
        return await _context.Voyages
            .Where(v => v.DeparturePortId == portId || v.ArrivalPortId == portId)
            .Select(v => v.Ship)
            .Distinct()
            .AsNoTracking()
            .ToListAsync();
    }
}