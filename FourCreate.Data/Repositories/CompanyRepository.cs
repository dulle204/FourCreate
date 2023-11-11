using FourCreate.Data.Abstractions;
using FourCreate.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FourCreate.Data.Repositories;
public class CompanyRepository : ICompanyRepository
{
    private readonly FourCreateDbContext dbContext;

    public CompanyRepository(FourCreateDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Company> GetCompany(int id)
    {
        var company = await dbContext.Companies.SingleOrDefaultAsync(c => c.Id == id);
        return company;
    }

    public async Task<List<Company>> GetCompanies(int[] ids)
    {
        var company = await dbContext.Companies.Where(x=> ids.Contains(x.Id)).ToListAsync();
        return company;
    }
}
