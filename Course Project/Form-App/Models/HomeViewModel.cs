using Form_App.Entity;
using System.ComponentModel.DataAnnotations;

namespace Form_App.Models
{
    public class HomeViewModel
    {
        [Display(Name = "Latest Templates")]
        public List<Template> LatestTemplates { get; set; }
        [Display(Name = "Popular Templates")]
        public List<Template> PopularTemplates { get; set; }
    }
}
