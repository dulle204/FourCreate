using FourCreate.Data.Abstractions;
using FourCreate.Data.Models;
using FourCreate.Domain.Abstractions;
using FourCreate.Domain.Models;
using FourCreate.Domain.Validations;

namespace FourCreate.Domain.CreateEmployeeHandlers;
public class AddEmployeeToCompanyHandler : ICreateEmployeeHandler
{
    private readonly ICompanyRepository companyRepository;
    private readonly IEmployeeRepository employeeRepository;
    private readonly ICreateEmployeeValidation createEmployeeValidation;

    public bool EmployeeExists => true;

    public AddEmployeeToCompanyHandler(
        ICompanyRepository companyRepository,
        IEmployeeRepository employeeRepository, 
        ICreateEmployeeValidation createEmployeeValidation)
    {
        this.companyRepository = companyRepository;
        this.employeeRepository = employeeRepository;
        this.createEmployeeValidation = createEmployeeValidation;
    }

    public async Task<DomainResult<Employee>> HandleCreateEmployee(CreateEmployee createEmployee)
    {
        var companies = await companyRepository.GetCompanies(createEmployee.CompanyIds);
        var validationResult = createEmployeeValidation.Validate(createEmployee, companies);
        if (!validationResult.IsSuccess)
        {
            return new(validationResult.ErrorMessage);
        }
        var employee = await employeeRepository.GetEmployee(createEmployee.Email);
        foreach (var company in companies)
        {
            company.Employees.Add(employee);
        }
        await companyRepository.UpdateBatch(companies);

        return new DomainResult<Employee>(employee);
    }
}
