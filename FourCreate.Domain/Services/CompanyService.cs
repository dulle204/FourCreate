using EntityFramework.Exceptions.Common;
using FourCreate.Data.Abstractions;
using FourCreate.Data.Models;
using FourCreate.Domain.Abstractions;

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
            // handle exception
            // change to record
            return new(null)
            {
                ErrorMessage = ex.Message,
                IsSuccess = false
            };
        }

        foreach (var item in createCompany.Employees)
        {
            var createEmployeeHandler = createEmployeeHandlerFactory.GetCreateEmployeeHandler(item.id.HasValue);
            await createEmployeeHandler.HandleCreateEmployee(new()
            {
                CompanyIds = new[] { newCompany.Id },
                Email = item.Email,
                Title = (EmployeeTitle)item.Title,
            });
        }

        return new(newCompany);
    }
}
