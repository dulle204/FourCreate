using FourCreate.Data.Abstractions;
using FourCreate.Data.Models;
using FourCreate.Domain.Abstractions;
using FourCreate.Domain.Models;

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
        

        var companies = await companyRepository.GetCompanies(createEmployee.CompanyIds);
        if (companies is null
            && companies.Count == 0)
        {
            return new($"Comapnies does not exist.");
        }

        foreach (var company in companies)
        {
            if (company.Employees is not null
                && company.Employees.Any(x => x.Title.Equals(createEmployee.Title)))
            {
                return new("Title already exists in company");
            }
        }
        var createEmployeHandler = await handlerFactory.GetCreateEmployeeHandler(createEmployee.Email);
        var result = await createEmployeHandler.HandleCreateEmployee(createEmployee);

        return result;
    }
}
