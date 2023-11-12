using FourCreate.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourCreate.MigrationRunner;
public class DbContextFactory : IDesignTimeDbContextFactory<FourCreateDbContext>
{
    public FourCreateDbContext CreateDbContext(string[] args)
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        environmentName = string.IsNullOrEmpty(environmentName) ? "Development" : environmentName;

        var configuration = new ConfigurationBuilder()
             .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
             .AddJsonFile($"appsettings.json", optional: true)
             .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
             .AddEnvironmentVariables();

        var config = configuration.Build();
        string connectionString = config.GetConnectionString("FourCreateDatabase");

        DbContextOptionsBuilder<FourCreateDbContext> dbContextOptionsBuilder = new DbContextOptionsBuilder<FourCreateDbContext>();

        dbContextOptionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

        var dbContext = new FourCreateDbContext(dbContextOptionsBuilder.Options);

        return dbContext;
    }
}
