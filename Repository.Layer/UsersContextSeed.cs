using Data.Layer.Contexts;
using Data.Layer.Entities;
using Data.Layer.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Repository.Layer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Layer
{
    public class UsersContextSeed
    {
        public static async Task SeedUsersAsync(AppDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ILogger logger = null)
        {
            // Ensure roles exist
            string[] roles = { "Admin", "User" };

            if (await context.Database.CanConnectAsync())
            {
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }

                if (!userManager.Users.Any())
                {
                    // Seed Admin User
                    if (await userManager.FindByEmailAsync("admin@demo.com") == null)
                    {
                        var adminUser = new AppUser
                        {
                            UserName = "admin@demo.com",
                            Email = "admin@demo.com",
                            DisplayName = "Admin",
                            Gender = "Male",
                            DateOfBirth = new DateOnly(1985, 1, 1),
                            Age = Extenstions.CalculateAge(new DateOnly(1985, 1, 1)),
                            LastActive = DateTime.UtcNow
                        };

                        var result = await userManager.CreateAsync(adminUser, "Admin123!");

                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(adminUser, "Admin");
                            logger?.LogInformation("Seeded Admin user.");
                        }
                        else
                        {
                            logger?.LogError("Failed to seed Admin user: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                        }
                    }

                    // Seed Regular Users
                    var usersToSeed = new List<AppUser>
                    {
                        new AppUser
                        {
                            UserName = "user@demo.com",
                            Email = "user@demo.com",
                            DisplayName = "Regular User",
                            Gender = "Female",
                            DateOfBirth = new DateOnly(1995, 5, 15),
                            Age = Extenstions.CalculateAge(new DateOnly(1995, 5, 15)),
                            LastActive = DateTime.UtcNow,
                            Address = new Address
                            {
                                Street = "123 Maple Street",
                                City = "Cairo",
                                State = "Cairo Governorate",
                                Country = "Egypt"
                            }
                        },
                        new AppUser
                        {
                            UserName = "john.doe@demo.com",
                            Email = "john.doe@demo.com",
                            DisplayName = "John Doe",
                            Gender = "Male",
                            DateOfBirth = new DateOnly(1990, 3, 10),
                            Age = Extenstions.CalculateAge(new DateOnly(1990, 3, 10)),
                            LastActive = DateTime.UtcNow,
                            Address = new Address
                            {
                                Street = "456 Elm Avenue",
                                City = "Dubai",
                                State = "Dubai",
                                Country = "UAE"
                            }
                        },
                        new AppUser
                        {
                            UserName = "sarah.smith@demo.com",
                            Email = "sarah.smith@demo.com",
                            DisplayName = "Sarah Smith",
                            Gender = "Female",
                            DateOfBirth = new DateOnly(1998, 7, 22),
                            Age = Extenstions.CalculateAge(new DateOnly(1998, 7, 22)),
                            LastActive = DateTime.UtcNow,
                            Address = new Address
                            {
                                Street = "789 Oak Blvd",
                                City = "Amman",
                                State = "Amman",
                                Country = "Jordan"
                            }
                        },
                        new AppUser
                        {
                            UserName = "mark.taylor@demo.com",
                            Email = "mark.taylor@demo.com",
                            DisplayName = "Mark Taylor",
                            Gender = "Male",
                            DateOfBirth = new DateOnly(1987, 11, 5),
                            Age = Extenstions.CalculateAge(new DateOnly(1987, 11, 5)),
                            LastActive = DateTime.UtcNow,
                            Address = new Address
                            {
                                Street = "159 Pine Drive",
                                City = "Riyadh",
                                State = "Riyadh",
                                Country = "Saudi Arabia"
                            }
                        },
                        new AppUser
                        {
                            UserName = "linda.jones@demo.com",
                            Email = "linda.jones@demo.com",
                            DisplayName = "Linda Jones",
                            Gender = "Female",
                            DateOfBirth = new DateOnly(1992, 9, 30),
                            Age = Extenstions.CalculateAge(new DateOnly(1992, 9, 30)),
                            LastActive = DateTime.UtcNow,
                            Address = new Address
                            {
                                Street = "62 Palm Court",
                                City = "Beirut",
                                State = "Beirut",
                                Country = "Lebanon"
                            }
                        },
                        new AppUser
                        {
                            UserName = "ali.khan@demo.com",
                            Email = "ali.khan@demo.com",
                            DisplayName = "Ali Khan",
                            Gender = "Male",
                            DateOfBirth = new DateOnly(1985, 1, 12),
                            Age = Extenstions.CalculateAge(new DateOnly(1985, 1, 12)),
                            LastActive = DateTime.UtcNow,
                            Address = new Address
                            {
                                Street = "88 Crescent Street",
                                City = "Karachi",
                                State = "Sindh",
                                Country = "Pakistan"
                            }
                        }
                    };


                    foreach (var user in usersToSeed)
                    {
                        if (await userManager.FindByEmailAsync(user.Email) == null)
                        {
                            var result = await userManager.CreateAsync(user, "User123!");

                            if (result.Succeeded)
                            {
                                await userManager.AddToRoleAsync(user, "User");
                                logger?.LogInformation($"Seeded user: {user.Email}");
                            }
                            else
                            {
                                logger?.LogError("Failed to seed user {Email}: {Errors}", user.Email, string.Join(", ", result.Errors.Select(e => e.Description)));
                            }
                        }
                    }
                }
            }
        }
    }
}
