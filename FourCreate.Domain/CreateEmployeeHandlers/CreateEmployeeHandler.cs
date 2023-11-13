using FourCreate.Data.Abstractions;
using FourCreate.Data.Models;
using FourCreate.Domain.Abstractions;
using FourCreate.Domain.Interfaces;
using FourCreate.Domain.Models;
using FourCreate.Domain.Validations;

namespace FourCreate.Domain.CreateEmployeeHandlers;
public class CreateEmployeeHandler : ICreateEmployeeHandler
{
    private readonly IEmployeeRepository employeeRepository;
    private readonly ICompanyRepository companyRepository;
    private readonly ISystemLogService systemLogService;
    private readonly ICreateEmployeeValidation createEmployeeValidation;

    public CreateEmployeeHandler(
        IEmployeeRepository employeeRepository,
        ICompanyRepository companyRepository,
        ISystemLogService systemLogService, 
        ICreateEmployeeValidation createEmployeeValidation)
    {
        this.employeeRepository = employeeRepository;
        this.companyRepository = companyRepository;
        this.systemLogService = systemLogService;
        this.createEmployeeValidation = createEmployeeValidation;
    }
    public bool EmployeeExists => false;

    public async Task<DomainResult<Employee>> HandleCreateEmployee(CreateEmployee createEmployee)
    {
        var comapnies = await companyRepository.GetCompanies(createEmployee.CompanyIds);
        var validationResult = createEmployeeValidation.Validate(createEmployee, comapnies);
        if (!validationResult.IsSuccess)
        {
            return new(validationResult.ErrorMessage);
        }

        Employee newEmployee = new()
        {
            Title = (Data.Models.EmployeeTitle)createEmployee.Title,
            Companies = comapnies,
            Email = createEmployee.Email
        };

        var insertedEmployee = await employeeRepository.InsertEmployee(newEmployee);

        CreateLog<Employee> createLog = new("create", "new employee was created", insertedEmployee);
        await systemLogService.CreateLog(createLog);

        return new DomainResult<Employee>(insertedEmployee);
    }
}
