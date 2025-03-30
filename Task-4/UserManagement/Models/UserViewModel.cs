using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Models
{
    public class UserViewModel
    {
        public List<User>? Users{ get; set; }
        public string? CurrentUserEmail{ get; set; }
    }
}