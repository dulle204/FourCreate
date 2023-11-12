using FourCreate.Data.Abstractions;
using FourCreate.Data.Models;
using FourCreate.Domain.Abstractions;

namespace FourCreate.Domain.Services;
public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository employeeRepository;
    private readonly ICompanyRepository companyRepository;
    private readonly ICreateEmployeeHandlerFactory handlerFactory;

    public EmployeeService(
        IEmployeeRepository employeeRepository,
        ICompanyRepository companyRepository,
        ICreateEmployeeHandlerFactory handlerFactory)
    {
        this.employeeRepository = employeeRepository;
        this.companyRepository = companyRepository;
        this.handlerFactory = handlerFactory;
    }

    public async Task<DomainResult<Employee>> CreateEmployee(CreateEmployee createEmployee)
    {
        var employee = await employeeRepository.GetEmployee(createEmployee.Email);
        if (employee is not null)
        {
            // handle employee exists
        }

        var companies = await companyRepository.GetCompanies(createEmployee.CompanyIds);
        if (companies is null
            && companies.Count == 0)
        {
            // companies not exist    
        }

        foreach (var company in companies)
        {
            if (company.Employees is not null
                && company.Employees.Any(x => x.Title.Equals(createEmployee.Title)))
            {
                // handle title exists
            }
        }
        var createEmployeHandler = handlerFactory.GetCreateEmployeeHandler(employee is not null);
        var result = await createEmployeHandler.HandleCreateEmployee(createEmployee);

        return result;
    }

    /*private async Task<DomainResult<object>> ValidateAsync(CreateEmployee createEmployee)
    {
        var employee = await employeeRepository.GetEmployee(createEmployee.Email);
        if (employee is not null)
        {
            // handle employee exists
        }

        var companies = await companyRepository.GetCompanies(createEmployee.CompanyIds);
        if (companies is null
            && companies.Count == 0)
        {
            // companies not exist    
        }

        foreach (var company in companies)
        {
            if (company.Employees is not null
                && company.Employees.Any(x => x.Title.Equals(createEmployee.Title)))
            {
                // handle title exists
            }
        }
    }*/
}
