namespace BeSmart.Domain.DTOs.Answer
{
    public class AnswerCreationDTO
    {
        public string Text { get; set; }
        public bool Fidelity { get; set; }
        public int? QuestionId { get; set; }
    }
}
