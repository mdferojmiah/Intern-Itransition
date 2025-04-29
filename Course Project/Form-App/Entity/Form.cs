using System.ComponentModel.DataAnnotations;

namespace Form_App.Entity
{
    public class Form
    {
        public int Id { get; set; }
        public int TemplateId { get; set; }
        [Required]
        public string RespondentId { get; set; }
        public DateTime SubmissionDate { get; set; } = DateTime.Now;

        //navigation properties
        public virtual Template Template { get; set; }
        public virtual ApplicationUser Respondent { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }

        public Form()
        {
            Answers = new HashSet<Answer>();
        }

    }
}
