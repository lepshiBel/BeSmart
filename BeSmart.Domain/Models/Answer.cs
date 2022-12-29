using BeSmart.Domain.Models.Base;

namespace BeSmart.Domain.Models
{
    public class Answer : EntityBase
    {
        public string Text { get; set; }
        public bool Fidelity { get; set; }
        public Question Question { get; set; }
        public int QuestionId { get; set; }
    }
}