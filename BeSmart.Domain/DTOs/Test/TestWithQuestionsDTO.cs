using BeSmart.Domain.DTOs.Question;

namespace BeSmart.Domain.DTOs
{
    public class TestWithQuestionsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<QuestionWithAnswersDTO> QuestionsWithAnswers { get; set; }
    }
}
