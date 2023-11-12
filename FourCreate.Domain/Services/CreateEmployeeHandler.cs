﻿using FourCreate.Data.Abstractions;
using FourCreate.Data.Models;
using FourCreate.Data.Repositories;
using FourCreate.Domain.Abstractions;
using FourCreate.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourCreate.Domain.Services;
public class CreateEmployeeHandler : ICreateEmployeeHandler
{
    private readonly IEmployeeRepository employeeRepository;
    private readonly ICompanyRepository companyRepository;
    private readonly ISystemLogService systemLogService;

    public CreateEmployeeHandler(
        IEmployeeRepository employeeRepository,
        ICompanyRepository companyRepository,
        ISystemLogService systemLogService)
    {
        this.employeeRepository = employeeRepository;
        this.companyRepository = companyRepository;
        this.systemLogService = systemLogService;
    }
    public bool EmployeeExists => false;

    public async Task<DomainResult<Employee>> HandleCreateEmployee(CreateEmployee createEmployee)
    {
        Employee newEmployee = new()
        {
            Title = (Data.Models.EmployeeTitle)createEmployee.Title,
            Companies = await companyRepository.GetCompanies(createEmployee.CompanyIds),
            Email = createEmployee.Email
        };

        var insertedEmployee = await employeeRepository.InsertEmployee(newEmployee);

        CreateLog<Employee> createLog = new("create", "new employee was created", insertedEmployee);
        await systemLogService.CreateLog(createLog);

        return new DomainResult<Employee>(insertedEmployee);
    }
}
