using FourCreate.Domain.Abstractions;

namespace FourCreate.Domain;
public interface ICreateEmployeeHandlerFactory
{
    ICreateEmployeeHandler GetCreateEmployeeHandler(bool employeeExists);
}