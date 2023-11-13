using FourCreate.Data.Models;
using FourCreate.Domain.Models;

namespace FourCreate.Domain.Abstractions;
public interface ICreateEmployeeValidation
{
    DomainResult<object> Validate(CreateEmployee createEmployee, List<Company> companies);
}