namespace FourCreate.Domain;
public class CreateEmployee
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
