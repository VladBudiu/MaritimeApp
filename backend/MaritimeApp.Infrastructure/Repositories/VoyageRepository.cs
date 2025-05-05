using MaritimeApp.Domain.Entities;
using MaritimeApp.Domain.Interfaces;
using MaritimeApp.Infrastructure.Data;

namespace MaritimeApp.Infrastructure.Repositories;

public class VoyageRepository : Repository<Voyage>, IVoyageRepository
{
    public VoyageRepository(MaritimeDbContext context) : base(context) { }
}