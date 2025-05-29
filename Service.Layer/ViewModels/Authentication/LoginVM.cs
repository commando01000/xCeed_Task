using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Layer.ViewModels.Authentication
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email is required"), EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required"), DataType(DataType.Password), MinLength(8), MaxLength(50)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
