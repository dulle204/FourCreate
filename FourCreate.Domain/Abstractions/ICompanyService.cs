using FourCreate.Data.Models;
using FourCreate.Domain.Models;

namespace FourCreate.Domain.Abstractions;
public interface ICompanyService
{
    Task<DomainResult<Company>> CreateCompany(CreateCompany createCompany);
}