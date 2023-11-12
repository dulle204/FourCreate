using FourCreate.Data.Models;

namespace FourCreate.Domain.Abstractions;
public interface ICompanyService
{
    Task<DomainResult<Company>> CreateCompany(CreateCompany createCompany);
}