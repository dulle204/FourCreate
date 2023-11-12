namespace FourCreate.API.Models.Requests;

public record CreateCompanyRequest(string Name, List<NewEmployee> Employees);

public record NewEmployee(int? id, EmployeeTitle? Title, string Email);