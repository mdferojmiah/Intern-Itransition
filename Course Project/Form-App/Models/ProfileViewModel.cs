using System.ComponentModel.DataAnnotations;

namespace Form_App.Models
{
    public class ProfileViewModel
    {
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
