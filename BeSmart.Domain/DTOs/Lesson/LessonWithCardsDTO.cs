using BeSmart.Domain.DTOs.Card;

namespace BeSmart.Domain.DTOs.Lesson
{
    public class LessonWithCardsDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public ICollection<CardDTO> Cards { get; set; }
    }
}
