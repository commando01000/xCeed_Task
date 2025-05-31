using Data.Layer.Contexts;
using Data.Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Layer
{
    public class TasksContextSeed
    {
        public static async Task SeedTasksAsync(AppDbContext context)
        {
            if (!context.Tasks.Any())
            {
                var admin = context.Users.FirstOrDefault(u => u.Email == "admin@demo.com");
                var user = context.Users.FirstOrDefault(u => u.Email == "user@demo.com");
                var john = context.Users.FirstOrDefault(u => u.Email == "john.doe@demo.com");
                var sarah = context.Users.FirstOrDefault(u => u.Email == "sarah.smith@demo.com");
                var mark = context.Users.FirstOrDefault(u => u.Email == "mark.taylor@demo.com");

                var tasks = new List<TaskItem>
                {
                    new TaskItem
                    {
                        TaskName = "Review Reports",
                        Description = "Review financial reports before Friday",
                        Priority = TaskPriority.High,
                        DueDate = DateTime.Today.AddDays(2),
                        AssignedUserId = admin?.Id
                    },
                    new TaskItem
                    {
                        TaskName = "Update Website",
                        Description = "Change homepage banner and update footer links",
                        Priority = TaskPriority.Medium,
                        DueDate = DateTime.Today.AddDays(5),
                        AssignedUserId = user?.Id
                    },
                    new TaskItem
                    {
                        TaskName = "Test Login Flow",
                        Description = "Verify login and registration flows with different roles",
                        Priority = TaskPriority.Low,
                        DueDate = DateTime.Today.AddDays(3),
                        AssignedUserId = user?.Id
                    },
                    new TaskItem
                    {
                        TaskName = "Design Brochure",
                        Description = "Create new marketing brochure for summer campaign",
                        Priority = TaskPriority.Medium,
                        DueDate = DateTime.Today.AddDays(6),
                        AssignedUserId = sarah?.Id
                    },
                    new TaskItem
                    {
                        TaskName = "Database Backup",
                        Description = "Schedule and test full backup routine",
                        Priority = TaskPriority.High,
                        DueDate = DateTime.Today.AddDays(1),
                        AssignedUserId = admin?.Id
                    },
                    new TaskItem
                    {
                        TaskName = "Social Media Calendar",
                        Description = "Plan Instagram content calendar for next 2 weeks",
                        Priority = TaskPriority.Low,
                        DueDate = DateTime.Today.AddDays(7),
                        AssignedUserId = john?.Id
                    },
                    new TaskItem
                    {
                        TaskName = "Prepare Training Material",
                        Description = "Create onboarding presentation and checklists",
                        Priority = TaskPriority.Medium,
                        DueDate = DateTime.Today.AddDays(10),
                        AssignedUserId = mark?.Id
                    },
                    new TaskItem
                    {
                        TaskName = "Team Feedback Survey",
                        Description = "Draft questions and launch internal survey",
                        Priority = TaskPriority.Low,
                        DueDate = DateTime.Today.AddDays(4),
                        AssignedUserId = null // Unassigned
                    },
                    new TaskItem
                    {
                        TaskName = "Fix Payment Bug",
                        Description = "Investigate and fix double-charge issue",
                        Priority = TaskPriority.High,
                        DueDate = DateTime.Today.AddDays(1),
                        AssignedUserId = john?.Id
                    },
                    new TaskItem
                    {
                        TaskName = "Office Inventory Check",
                        Description = "Audit hardware and supply levels",
                        Priority = TaskPriority.Medium,
                        DueDate = DateTime.Today.AddDays(8),
                        AssignedUserId = null
                    }
                };

                await context.Tasks.AddRangeAsync(tasks);
                await context.SaveChangesAsync();
            }
        }
    }
}
