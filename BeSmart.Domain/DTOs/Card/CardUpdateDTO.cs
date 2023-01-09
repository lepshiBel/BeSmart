namespace BeSmart.Domain.DTOs.Card
{
    public class CardUpdateDTO
    {
        public int Id { get; set; }
        public string Word { get; set; }

        public string? Text { get; set; }

        public string? ImageUrl { get; set; }

        public string? Transctipt { get; set; }
    }
}
