using System.ComponentModel.DataAnnotations;
using Form_App.Entity.Enums;

namespace Form_App.Models
{
    public class EditQuestionViewModel
    {
        public int Id { get; set; }

        public int TemplateId { get; set; }

        [Required(ErrorMessage = "Question title is required")]
        [Display(Name = "Question Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Question type is required")]
        [Display(Name = "Question Type")]
        public QuestionType QuestionType { get; set; }

        [Display(Name = "Show In Results")]
        public bool ShowInResults { get; set; }
    }
}
