namespace FourCreate.Domain.Models;
public record CreateCompany(string Name, List<NewEmployee> Employees);

public record NewEmployee(int? id, EmployeeTitle? Title, string Email);
