using FourCreate.Data.Models;
using FourCreate.Domain.Abstractions;
using FourCreate.Domain.Models;

namespace FourCreate.Domain.Services;
public class EmployeeService : IEmployeeService
{
    private readonly ICreateEmployeeHandlerFactory handlerFactory;

    public EmployeeService(ICreateEmployeeHandlerFactory handlerFactory)
    {
        this.handlerFactory = handlerFactory;
    }

    public async Task<DomainResult<Employee>> CreateEmployee(CreateEmployee createEmployee)
    {
        var createEmployeHandler = await handlerFactory.GetCreateEmployeeHandler(createEmployee.Email);
        var result = await createEmployeHandler.HandleCreateEmployee(createEmployee);

        return result;
    }
}
