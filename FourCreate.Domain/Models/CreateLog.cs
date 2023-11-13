using FourCreate.Data.Models;

namespace FourCreate.Domain.Models;
public record CreateLog<T>(string Event, string Comment, T Entity) where T : BaseEntity;
