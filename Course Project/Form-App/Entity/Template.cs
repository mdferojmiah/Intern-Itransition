using System.ComponentModel.DataAnnotations;

namespace Form_App.Entity
{
    public class Template
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        public string CreatorId { get; set; }
        public string CreationDate { get; set; }
        [Display(Name = "Public Access")]
        public bool IsPublic { get; set; } = true;

        //navigation properties
        public virtual ApplicationUser Creator { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Form> Forms { get; set; }

        public Template()
        {
            Questions = new HashSet<Question>();
            Forms = new HashSet<Form>();
        }
    }
}
