using FourCreate.Data.Models;
using FourCreate.Domain.Models;

namespace FourCreate.Domain.Abstractions;
public interface IEmployeeService
{
    Task<DomainResult<Employee>> CreateEmployee(CreateEmployee createEmployee);
}