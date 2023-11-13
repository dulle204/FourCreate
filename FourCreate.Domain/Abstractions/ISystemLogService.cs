using FourCreate.Data.Models;
using FourCreate.Domain.Models;

namespace FourCreate.Domain.Interfaces;
public interface ISystemLogService
{
    Task CreateLog<T>(CreateLog<T> createLog) where T : BaseEntity;
}