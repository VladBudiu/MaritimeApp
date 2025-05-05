using MaritimeApp.Domain.Entities;
using MaritimeApp.Domain.Interfaces;
using MaritimeApp.Infrastructure.Data;

namespace MaritimeApp.Infrastructure.Repositories;


public class CompanyRepository : Repository<Company>, ICompanyRepository
{
    public CompanyRepository(MaritimeDbContext context) : base(context) { }
}