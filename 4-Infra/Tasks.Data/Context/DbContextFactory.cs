using Tasks.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

public class DbContextFactory : IDesignTimeDbContextFactory<TasksContext>
{
    public TasksContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<TasksContext>();

        var ent = Directory.GetCurrentDirectory();

        var sharedFolder = Path.Combine(ent, "..", "..", "..", "0-Presentations", "0.1-WebApi", "Tasks.WebApi", "appsettings.json");

        IConfigurationRoot configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile(sharedFolder)
          .Build();

        builder.UseSqlServer(configuration.GetConnectionString("Defa‌​ultConnection"));
        return new TasksContext(builder.Options);
    }
}