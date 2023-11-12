using FourCreate.Data.Models;

namespace FourCreate.Domain.Abstractions;
public interface IEmployeeService
{
    Task<DomainResult<Employee>> CreateEmployee(CreateEmployee createEmployee);
}