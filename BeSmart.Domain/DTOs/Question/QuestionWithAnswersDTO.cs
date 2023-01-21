using BeSmart.Domain.DTOs.Answer;

namespace BeSmart.Domain.DTOs.Question
{
    public class QuestionWithAnswersDTO
    {
        public int Id { get; set; } 
        public string Text { get; set; }
        public ICollection<AnswerDTO> Answers { get; set; }
    }
}
