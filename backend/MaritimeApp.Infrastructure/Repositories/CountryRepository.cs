using MaritimeApp.Domain.Entities;
using MaritimeApp.Domain.Interfaces;
using MaritimeApp.Infrastructure.Data;

namespace MaritimeApp.Infrastructure.Repositories;

public class CountryRepository : Repository<Country>, ICountryRepository
{
    public CountryRepository(MaritimeDbContext context) : base(context) { }
}
