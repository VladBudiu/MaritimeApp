using MaritimeApp.Domain.Entities;
using MaritimeApp.Domain.Interfaces;
using MaritimeApp.Infrastructure.Data;

namespace MaritimeApp.Infrastructure.Repositories;

public class VisitedCountryRepository : Repository<VisitedCountry>, IVisitedCountryRepository
{
    public VisitedCountryRepository(MaritimeDbContext context) : base(context) { }
}