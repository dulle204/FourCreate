using FourCreate.Data.Models;

namespace FourCreate.Data.Abstractions;
public interface ICompanyRepository
{
    Task<List<Company>> GetCompanies(int[] ids);
    Task<Company> GetCompany(int id);
    Task<Company> Insert(Company company);
    Task UpdateBatch(List<Company> companies);
}