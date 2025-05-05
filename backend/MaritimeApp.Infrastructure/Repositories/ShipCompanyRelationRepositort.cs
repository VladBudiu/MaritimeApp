using MaritimeApp.Domain.Entities;
using MaritimeApp.Domain.Interfaces;
using MaritimeApp.Infrastructure.Data;

namespace MaritimeApp.Infrastructure.Repositories;

public class ShipCompanyRelationRepository : Repository<ShipCompanyRelation>, IShipCompanyRelationRepository
{
    public ShipCompanyRelationRepository(MaritimeDbContext context) : base(context) { }
}
