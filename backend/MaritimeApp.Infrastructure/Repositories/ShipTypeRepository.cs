using MaritimeApp.Domain.Entities;
using MaritimeApp.Domain.Interfaces;
using MaritimeApp.Infrastructure.Data;

namespace MaritimeApp.Infrastructure.Repositories;

public class ShipTypeRepository : Repository<ShipType>, IShipTypeRepository
{
    public ShipTypeRepository(MaritimeDbContext context) : base(context) { }
}