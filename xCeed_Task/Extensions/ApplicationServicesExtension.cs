using Data.Layer.Contexts;
using Microsoft.EntityFrameworkCore;
using xCeed_Task.Middlewares;

namespace xCeed_Task.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {

            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
            config.GetConnectionString("DefaultConnection"),
            sqlOptions => sqlOptions
            .MigrationsAssembly("Data.Layer")
            ));

            // Register exception middleware
            services.AddTransient<ExceptionMiddleware>();

            return services;
        }
    }
}
