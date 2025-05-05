using MaritimeApp.Domain.Entities;
using MaritimeApp.Domain.Interfaces;
using MaritimeApp.Infrastructure.Data;

namespace MaritimeApp.Infrastructure.Repositories;

    public class ShipLocationLogRepository : Repository<ShipLocationLog>, IShipLocationLogRepository
    {
        public ShipLocationLogRepository(MaritimeDbContext context) : base(context) { }
    }
