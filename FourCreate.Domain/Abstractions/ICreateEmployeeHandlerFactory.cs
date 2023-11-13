using FourCreate.Domain.Abstractions;

namespace FourCreate.Domain;
public interface ICreateEmployeeHandlerFactory
{
    Task<ICreateEmployeeHandler> GetCreateEmployeeHandler(string email);
    Task<ICreateEmployeeHandler> GetCreateEmployeeHandler(int id);
    Task<ICreateEmployeeHandler> GetCreateEmployeeHandler(int? id, string email);
}