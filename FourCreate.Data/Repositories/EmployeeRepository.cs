using FourCreate.Data.Abstractions;
using FourCreate.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourCreate.Data.Repositories;
public class EmployeeRepository : IEmployeeRepository
{
    private readonly FourCreateDbContext dbContext;

    public EmployeeRepository(FourCreateDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Employee> GetEmployee(string email)
    {
        var employee = await dbContext.Employees.SingleOrDefaultAsync(x => x.Email == email);
        return employee;
    }

    public async Task<Employee> GetEmployee(int id)
    {
        var employee = await dbContext.Employees.SingleOrDefaultAsync(x => x.Id == id);
        return employee;
    }

    public async Task<Employee> InsertEmployee(Employee employee)
    {
        var newEmployee = dbContext.Employees.Add(employee);
        await dbContext.SaveChangesAsync();
        return newEmployee.Entity;
    }

    public async Task<Employee> UpdateEmployee(Employee employee)
    {
        var updatedEmployee = dbContext.Employees.Update(employee);
        await dbContext.SaveChangesAsync();
        return updatedEmployee.Entity;
    }
}
