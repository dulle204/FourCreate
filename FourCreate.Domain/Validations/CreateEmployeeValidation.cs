using FourCreate.Data.Models;
using FourCreate.Domain.Abstractions;
using FourCreate.Domain.Models;

namespace FourCreate.Domain.Validations;
public class CreateEmployeeValidation : ICreateEmployeeValidation
{
    public DomainResult<object> Validate(CreateEmployee createEmployee, List<Company> companies)
    {
        if (companies is null
            && companies.Count == 0)
        {
            return new($"Comapnies does not exist.");
        }

        foreach (var company in companies)
        {
            if (company.Employees is not null
                && company.Employees.Any(x => x.Title.Equals((Data.Models.EmployeeTitle)createEmployee.Title)))
            {
                return new("Title already exists in company");
            }
        }

        return new(null as object);
    }
}
