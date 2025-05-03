using System.ComponentModel.DataAnnotations;

namespace Form_App.Models
{
    public class TemplateViewModel
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

        public string CreatorName { get; set; }
        public string CreationDate { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
    }
}
