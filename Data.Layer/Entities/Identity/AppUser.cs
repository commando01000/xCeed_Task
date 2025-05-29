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
        public string? DisplayName { get; set; } // AkA known as
        public DateOnly DateOfBirth { get; set; }
        public DateTime LastActive { get; set; } = DateTime.Now;
        public string? Gender { get; set; }
        public int Age { get; set; }
    }
}
