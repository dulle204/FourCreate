using FourCreate.Data.Abstractions;
using FourCreate.Data.Models;
using FourCreate.Domain.Abstractions;
using FourCreate.Domain.Models;

namespace FourCreate.Domain.CreateEmployeeHandlers;
public class AddEmployeeToCompanyHandler : ICreateEmployeeHandler
{
    private readonly ICompanyRepository companyRepository;
    private readonly IEmployeeRepository employeeRepository;

    public bool EmployeeExists => true;

    public AddEmployeeToCompanyHandler(
        ICompanyRepository companyRepository,
        IEmployeeRepository employeeRepository)
    {
        this.companyRepository = companyRepository;
        this.employeeRepository = employeeRepository;
    }

    public async Task<DomainResult<Employee>> HandleCreateEmployee(CreateEmployee createEmployee)
    {
        var companies = await companyRepository.GetCompanies(createEmployee.CompanyIds);
        var employee = await employeeRepository.GetEmployee(createEmployee.Email);
        foreach (var company in companies)
        {
            company.Employees.Add(employee);
        }
        await companyRepository.UpdateBatch(companies);

        return new DomainResult<Employee>(employee);
    }
}
