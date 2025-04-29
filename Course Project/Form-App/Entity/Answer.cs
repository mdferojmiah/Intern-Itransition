using System.ComponentModel.DataAnnotations;

namespace Form_App.Entity
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int FormId { get; set; }
        [StringLength(2000)]
        public string StringValue { get; set; }
        public int? IntValue { get; set; }
        public bool? BoolValue { get; set; }

        //navigation properties
        public virtual Question Question { get; set; }
        public virtual Form Form { get; set; }
    }
}
