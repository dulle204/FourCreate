namespace FourCreate.Domain.Abstractions;
public interface ICreateEmployeeHandlerFactory
{
    Task<ICreateEmployeeHandler> GetCreateEmployeeHandler(string email);
    Task<ICreateEmployeeHandler> GetCreateEmployeeHandler(int id);
    Task<ICreateEmployeeHandler> GetCreateEmployeeHandler(int? id, string email);
}