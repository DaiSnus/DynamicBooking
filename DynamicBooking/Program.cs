using DynamicBooking.Infrastructure.Abstractions;
using DynamicBooking.Infrastructure.DataAccess;
using DynamicBooking.Infrastructure.Implementations;
using DynamicBooking.Initializers;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;

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
            app.UseSession();

            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI();

            app.MapControllers();
            app.MapDefaultControllerRoute();
            app.MapHealthChecks("healt-check");

            app.Run();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue;
                x.MultipartHeadersLengthLimit = int.MaxValue;
            });

            services.Configure<KestrelServerOptions>(options =>
            {
                options.Limits.MaxRequestBodySize = int.MaxValue;
            });

            services.AddHealthChecks();
            services.AddSwaggerGen();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAutoMapper(typeof(Program).Assembly);
            services.AddMediatR(o => o.RegisterServicesFromAssembly(typeof(Program).Assembly));

            services.AddControllersWithViews();
            services.AddControllersWithViews()
                    .AddSessionStateTempDataProvider();

            services.AddSession();

            services.AddScoped<IAppDbContext, AppDbContext>();
            services.AddScoped<IFileSaver,  FileSaver>();
            services.AddScoped<IFileDeleter, FIleDeleter>();

            DbContextInitializers.AddAppDbContext(services);
        }
    }
}
