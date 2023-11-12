using FourCreate.MigrationRunner;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Running migrations");

var dbContext = new DbContextFactory().CreateDbContext(args);

await dbContext.Database.MigrateAsync();

Console.WriteLine("Finishing DB updates");