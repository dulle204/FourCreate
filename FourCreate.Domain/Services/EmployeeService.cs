using FourCreate.Data.Abstractions;
using FourCreate.Data.Models;
using FourCreate.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FourCreate.Domain.Services;
public class EmployeeService
{
    private readonly IEmployeeRepository employeeRepository;
    private readonly ICompanyRepository companyRepository;
    private readonly ISystemLogRepository systemLogRepository;
    private readonly ISystemLogService systemLogService;

    public EmployeeService(
        IEmployeeRepository employeeRepository,
        ICompanyRepository companyRepository,
        ISystemLogRepository systemLogRepository, 
        ISystemLogService systemLogService)
    {
        this.employeeRepository = employeeRepository;
        this.companyRepository = companyRepository;
        this.systemLogRepository = systemLogRepository;
        this.systemLogService = systemLogService;
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

        Employee newEmployee = new()
        {
            Title = (Data.Models.EmployeeTitle)createEmployee.Title,
            Companies = companies,
            Email = createEmployee.Email
        };
        
        var insertedEmployee = await employeeRepository.InsertEmployee(newEmployee);
        
        CreateLog<Employee> createLog = new("create", "new employee was created", insertedEmployee);
        await systemLogService.CreateLog(createLog);

        return new DomainResult<Employee>(insertedEmployee);
    }

    private async Task<DomainResult<object>> ValidateAsync(CreateEmployee createEmployee)
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
    }
}
