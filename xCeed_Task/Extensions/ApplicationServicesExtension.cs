using Data.Layer.Contexts;
using Microsoft.EntityFrameworkCore;
using Repository.Layer;
using Repository.Layer.Interfaces;
using Service.Layer.Profiles;
using Service.Layer.Services.Tasks;
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

            // Register UnitOfWork with AppDbContext
            services.AddScoped(typeof(IUnitOfWork<AppDbContext>), typeof(UnitOfWork<AppDbContext>));

            // Register services
            services.AddScoped<ITaskService, TaskService>();

            // Register AutoMappers
            services.AddAutoMapper(typeof(UserProfile).Assembly);
            services.AddAutoMapper(typeof(TaskProfile).Assembly);

            return services;
        }
    }
}
