using Form_App.Entity;
using System.ComponentModel.DataAnnotations;

namespace Form_App.Models
{
    public class SearchViewModel
    {
        [Required(ErrorMessage = "Search query is required")]
        [MinLength(2, ErrorMessage = "Search query must be at least 2 characters")]
        [MaxLength(100, ErrorMessage = "Search query cannot exceed 100 characters")]
        [Display(Name = "Search Query")]
        public string Query { get; set; }

        [Display(Name = "Search Results")]
        public List<Template> Results { get; set; }
    }
}
