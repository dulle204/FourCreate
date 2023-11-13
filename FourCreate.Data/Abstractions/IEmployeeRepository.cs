using FourCreate.Data.Models;

namespace FourCreate.Data.Abstractions;
public interface IEmployeeRepository
{
    Task<Employee> GetEmployee(string email);
    Task<Employee> GetEmployee(int id);
    Task<Employee> InsertEmployee(Employee employee);
    Task<Employee> UpdateEmployee(Employee employee);
}