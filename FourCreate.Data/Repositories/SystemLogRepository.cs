using FourCreate.Data.Abstractions;
using FourCreate.Data.Models;

namespace FourCreate.Data.Repositories;
public class SystemLogRepository : ISystemLogRepository
{
    private readonly FourCreateDbContext dbContext;

    public SystemLogRepository(FourCreateDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<SystemLog> InsertLog(SystemLog systemLog)
    {
        var newSystemLog = dbContext.SystemLogs.Add(systemLog);
        await dbContext.SaveChangesAsync();
        return newSystemLog.Entity;
    }
}
