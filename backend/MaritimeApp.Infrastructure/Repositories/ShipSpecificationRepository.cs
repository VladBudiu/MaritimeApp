using MaritimeApp.Domain.Entities;
using MaritimeApp.Domain.Interfaces;
using MaritimeApp.Infrastructure.Data;

namespace MaritimeApp.Infrastructure.Repositories;

public class ShipSpecificationRepository : Repository<ShipSpecification>, IShipSpecificationRepository
{
    public ShipSpecificationRepository(MaritimeDbContext context) : base(context) { }
}