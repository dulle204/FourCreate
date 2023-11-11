namespace FourCreate.Data.Models;
public class Company : BaseEntity
{
    public string Name { get; set; }
    public List<Employee> Employees { get; set; }
}
