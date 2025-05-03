using System.ComponentModel.DataAnnotations;

namespace Form_App.Models
{
    public class EditTemplateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        [Display(Name = "Template Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Make Public")]
        public bool IsPublic { get; set; }

        // Used for displaying questions in the edit view
        public List<QuestionViewModel> Questions { get; set; } = new List<QuestionViewModel>();
    }
}
