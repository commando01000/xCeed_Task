using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Data.Layer.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public string? DisplayName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public DateTime LastActive { get; set; } = DateTime.Now;
        public int Age { get; set; }
        public Address? Address { get; set; }
        public ICollection<TaskItem> AssignedTasks { get; set; } = new List<TaskItem>();
    }
}
