using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Name is required!")]
        public required string Name{ get; set; }
        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress(ErrorMessage = "Invalid email format!")]
        public required string Email{ get; set; }
        [Required(ErrorMessage = "Password is required!")]
        public required string Password{ get; set; }
    }
}