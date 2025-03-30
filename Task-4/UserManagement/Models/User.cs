using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Models
{
    public class User
    {
        public int Id{ get; set; }
        [Required]
        public string? Name{ get; set; }
        [Required]
        [EmailAddress]
        public string? Email{ get; set; }
        [Required]
        public string? Password{ get; set; }
        public DateTime RegistrationTime{ get; set; }
        public DateTime? LastLoginTime{ get; set; }
        public string Status{ get; set; } = "active";
        public bool IsDeleted{ get; set; } = false;
    }
}