using MaritimeApp.Domain.Entities;
using MaritimeApp.Domain.Interfaces;
using MaritimeApp.Infrastructure.Data;

namespace MaritimeApp.Infrastructure.Repositories;

    public class ShipRepository : Repository<Ship>, IShipRepository
    {
        public ShipRepository(MaritimeDbContext context) : base(context) { }
    }
