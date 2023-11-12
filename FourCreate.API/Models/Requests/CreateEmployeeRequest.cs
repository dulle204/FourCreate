namespace FourCreate.API.Models.Requests;

public class CreateEmployeeRequest
{
    public string Email { get; set; }
    public EmployeeTitle Title { get; set; }
    public int[] CompanyIds { get; set; }
}

public enum EmployeeTitle
{
    Developer,
    Manager,
    Tester
}
