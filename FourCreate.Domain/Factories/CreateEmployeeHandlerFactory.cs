using FourCreate.Data.Abstractions;
using FourCreate.Domain.Abstractions;
using FourCreate.Domain.CreateEmployeeHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace FourCreate.Domain;
public class CreateEmployeeHandlerFactory : ICreateEmployeeHandlerFactory
{
    private readonly List<ICreateEmployeeHandler> createEmployeeHandlers;
    private readonly IEmployeeRepository employeeRepository;

    public CreateEmployeeHandlerFactory(
        IServiceProvider serviceProvider,
        IEmployeeRepository employeeRepository)
    {
        var handlers = new[]
        {
            typeof(CreateEmployeeHandler),
            typeof(AddEmployeeToCompanyHandler)
        };

        createEmployeeHandlers = handlers.Select(x => (ICreateEmployeeHandler)serviceProvider.GetRequiredService(x)).ToList();
        this.employeeRepository = employeeRepository;
    }

    public async Task<ICreateEmployeeHandler> GetCreateEmployeeHandler(string email)
    {
        var employee = await employeeRepository.GetEmployee(email);
        return createEmployeeHandlers.Single(x => employee is not null);
    }

    public async Task<ICreateEmployeeHandler> GetCreateEmployeeHandler(int id)
    {
        var employee = await employeeRepository.GetEmployee(id);
        return createEmployeeHandlers.Single(x => employee is not null);
    }

    public async Task<ICreateEmployeeHandler> GetCreateEmployeeHandler(int? id, string email)
    {
        if (id.HasValue)
        {
            return await GetCreateEmployeeHandler(id.Value);
        }
        else if (!string.IsNullOrEmpty(email)) 
        { 
            return await GetCreateEmployeeHandler(email);
        }
        else
        {
            throw new ArgumentException("Invalid arguments to provide right handler.");
        }
    }
}
