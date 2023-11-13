using FourCreate.Data.Models;
using FourCreate.Domain.Models;

namespace FourCreate.Domain.Abstractions;
public interface ICreateEmployeeHandler
{
    bool EmployeeExists { get; }

    Task<DomainResult<Employee>> HandleCreateEmployee(CreateEmployee createEmployee);
}
