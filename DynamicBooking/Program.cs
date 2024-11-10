using DynamicBooking.Infrastructure.Abstractions;
using DynamicBooking.Infrastructure.DataAccess;
using DynamicBooking.Initializers;

namespace DynamicBooking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder.Services);

            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            using var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            DbContextInitializers.InitializeDbContext(appDbContext);

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.MapControllers();
            app.MapHealthChecks("healt-check");

            app.Run();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
            services.AddSwaggerGen();

            services.AddAutoMapper(typeof(Program).Assembly);
            services.AddMediatR(o => o.RegisterServicesFromAssembly(typeof(Program).Assembly));

            services.AddControllersWithViews();
            
            services.AddScoped<IAppDbContext, AppDbContext>();

            DbContextInitializers.AddAppDbContext(services);
        }
    }
}
