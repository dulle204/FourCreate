using EntityFramework.Exceptions.Common;
using FourCreate.Data.Abstractions;
using FourCreate.Data.Models;
using FourCreate.Domain.Abstractions;
using FourCreate.Domain.Models;

namespace FourCreate.Domain.Services;
public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository companyRepository;
    private readonly ICreateEmployeeHandlerFactory createEmployeeHandlerFactory;

    public CompanyService(
        ICompanyRepository companyRepository,
        ICreateEmployeeHandlerFactory createEmployeeHandlerFactory)
    {
        this.companyRepository = companyRepository;
        this.createEmployeeHandlerFactory = createEmployeeHandlerFactory;
    }

    public async Task<DomainResult<Company>> CreateCompany(CreateCompany createCompany)
    {
        Company newCompany;
        try
        {
            newCompany = await companyRepository.Insert(new()
            {
                Name = createCompany.Name,
                CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            });
        }
        catch (UniqueConstraintException ex)
        {
            return new(ex.Message);
        }

        foreach (var item in createCompany.Employees)
        {
            var createEmployeeHandler = await createEmployeeHandlerFactory.GetCreateEmployeeHandler(item.id, item.Email);
            await createEmployeeHandler.HandleCreateEmployee(new()
            {
                CompanyIds = new[] { newCompany.Id },
                Email = item.Email,
                Title = (Models.EmployeeTitle)item.Title,
            });
        }

        return new(newCompany);
    }
}
