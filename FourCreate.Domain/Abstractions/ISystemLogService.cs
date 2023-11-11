using FourCreate.Data.Models;

namespace FourCreate.Domain.Interfaces;
public interface ISystemLogService
{
    Task CreateLog<T>(CreateLog<T> createLog) where T : BaseEntity;
}