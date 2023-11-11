using FourCreate.Data.Abstractions;
using FourCreate.Data.Models;
using FourCreate.Domain.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FourCreate.Domain.Services;
public class SystemLogService : ISystemLogService
{
    private readonly ISystemLogRepository systemLogRepository;

    public SystemLogService(ISystemLogRepository systemLogRepository)
    {
        this.systemLogRepository = systemLogRepository;
    }

    public async Task CreateLog<T>(CreateLog<T> createLog) where T : BaseEntity
    {
        JsonSerializerOptions options = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = true
        };
        SystemLog systemLog = new()
        {
            Comment = createLog.Comment,
            Event = createLog.Event,
            ResourceChangeset = JsonSerializer.Serialize(createLog.Entity, options),
            ResourceIdentifier = createLog.Entity.Id.ToString(),
            ResourceType = createLog.Entity.GetType().Name
        };
        await systemLogRepository.InsertLog(systemLog);
    }
}
