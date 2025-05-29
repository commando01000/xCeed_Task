using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Data.Layer.Contexts;
using xCeed_Task.Middlewares;
using xCeed_Task.Extensions;
using Data.Layer.Entities.Identity;
using Repository.Layer;
namespace xCeed_Task
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.AddApplicationServices(builder.Configuration);

            var app = builder.Build();

            // Seed the database and apply pending migrations if needed
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<UsersContextSeed>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var UserManager = services.GetRequiredService<UserManager<AppUser>>();
                try
                {
                    var context = services.GetRequiredService<AppDbContext>();

                    // Apply pending migrations
                    await context.Database.MigrateAsync();

                    // Seed the database
                    await UsersContextSeed.SeedUsersAsync(context, UserManager, roleManager, logger);
                }
                catch (Exception ex)
                {
                    var startupLogger = services.GetRequiredService<ILogger<Program>>();
                    startupLogger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            // Use the exception middleware early in the pipeline
            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
