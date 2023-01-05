using BeSmart.Domain.DTOs.Answer;

namespace BeSmart.Domain.DTOs
{
    public class TestWithQuestionsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<AnswerDTO> Answers { get; set; }
    }
}
