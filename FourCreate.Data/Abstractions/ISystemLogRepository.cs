using FourCreate.Data.Models;

namespace FourCreate.Data.Abstractions;
public interface ISystemLogRepository
{
    Task<SystemLog> InsertLog(SystemLog systemLog);
}