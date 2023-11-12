using FourCreate.Domain.Abstractions;
using FourCreate.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FourCreate.Domain;
public class CreateEmployeeHandlerFactory : ICreateEmployeeHandlerFactory
{
    private readonly List<ICreateEmployeeHandler> createEmployeeHandlers;

    public CreateEmployeeHandlerFactory(IServiceProvider serviceProvider)
    {
        var handlers = new[]
        {
            typeof(CreateEmployeeHandler),
            typeof(AddEmployeeToCompanyHandler)
        };

        createEmployeeHandlers = handlers.Select(x => (ICreateEmployeeHandler)serviceProvider.GetRequiredService(x)).ToList();
    }

    public ICreateEmployeeHandler GetCreateEmployeeHandler(bool employeeExists)
    {
        return createEmployeeHandlers.Single(x => x.EmployeeExists == employeeExists);
    }
}
