using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Form_App.Entity
{
    public class ApplicationUser: IdentityUser
    {
        [Display(Name = "Is Blocked")]
        public bool IsBlocked { get; set; } = false;

        //naviagtion properties
        public virtual ICollection<Template> Templates { get; set; }
        public virtual ICollection<Form> Forms { get; set; }

        public ApplicationUser() {
            Templates = new HashSet<Template>();
            Forms = new HashSet<Form>();
        }
    }
}
