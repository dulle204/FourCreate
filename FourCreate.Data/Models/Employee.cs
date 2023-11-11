namespace FourCreate.Data.Models;
public class Employee : BaseEntity
{
    public EmployeeTitle Title { get; set; }
    public string Email { get; set; }
    public List<Company> Companies { get; set; }
}

public enum EmployeeTitle
{
    Developer, 
    Manager, 
    Tester
}