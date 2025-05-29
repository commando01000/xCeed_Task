using Data.Layer.Contexts;
using Data.Layer.Entities.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Service.Layer.Services.Account;
using System.Text;

namespace xCeed_Task.Extensions
{
    public static class IdentityServicesExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            // Add Identity with the AppUser and IdentityRole types
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                // Configure Identity options (optional)
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;

                options.User.RequireUniqueEmail = true; // Ensure emails are unique
            })
            .AddEntityFrameworkStores<AppDbContext>() // Use AppDbContext for Identity
            .AddDefaultTokenProviders(); // Add default token providers (e.g., for email confirmation)

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Account/Login";
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.ExpireTimeSpan = TimeSpan.FromDays(15); // keep user logged in
                options.SlidingExpiration = true; // refresh expiration on activity
            });

            services.AddScoped<IAccountService, AccountService>();

            return services;
        }
    }
}
