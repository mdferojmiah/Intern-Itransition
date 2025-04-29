using System.ComponentModel.DataAnnotations;
using Form_App.Entity.Enums;

namespace Form_App.Entity
{
    public class Question
    {
        public int Id { get; set; }
        public int TemplateId { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public int OrderIndex { get; set; }
        [Required]
        [Display(Name = "Question Type")]
        public QuestionType QuestionType { get; set; }
        [Display(Name = "Show in Results")]
        public bool ShowInResults { get; set; } = true;

        //navigation properties
        public virtual Template Template { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }

        public Question()
        {
            Answers = new HashSet<Answer>();
        }
    }
}
