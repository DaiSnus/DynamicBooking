using DynamicBooking.Infrastructure.Abstractions;
using DynamicBooking.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DynamicBooking.Initializers;

public static class DbContextInitializers
{
    public static void AddAppDbContext(IServiceCollection services)
    {
        var pathToDbFile = GetPathToDbFile();

        services.AddDbContext<AppDbContext>(options => options
                    .UseSqlite($"Data Source={pathToDbFile}"));

        string GetPathToDbFile()
        {
            var appFolder = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData), "DynamicBooking");

            if (!Directory.Exists(appFolder))
            {
                Directory.CreateDirectory(appFolder);
            }

            return Path.Combine(appFolder, "DynamicBooking.db");
        }
    }

    public static void InitializeDbContext(AppDbContext appDbContext)
    {
        appDbContext.Database.Migrate();

        appDbContext.SaveChanges();
    }
}
