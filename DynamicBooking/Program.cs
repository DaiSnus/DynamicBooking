using DynamicBooking.Infrastructure.Abstractions;
using DynamicBooking.Infrastructure.DataAccess;
using DynamicBooking.Infrastructure.Implementations;
using DynamicBooking.Initializers;
using Microsoft.AspNetCore.Http.Features;

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
            app.MapDefaultControllerRoute();
            app.MapHealthChecks("healt-check");

            app.Run();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 104857600;
            });

            services.AddHealthChecks();
            services.AddSwaggerGen();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAutoMapper(typeof(Program).Assembly);
            services.AddMediatR(o => o.RegisterServicesFromAssembly(typeof(Program).Assembly));

            services.AddControllersWithViews();

            services.AddScoped<ICurrentUserAccessor, CurrentUserAccessor>();
            services.AddScoped<IAppDbContext, AppDbContext>();

            DbContextInitializers.AddAppDbContext(services);
        }
    }
}
