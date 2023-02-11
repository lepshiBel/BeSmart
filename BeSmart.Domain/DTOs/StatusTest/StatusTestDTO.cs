
namespace BeSmart.Domain.DTOs.StatusTest
{
    public class StatusTestDTO
    {
        public int Id { get; set; }
        public string? TestStatus { get; set; }
        public string? TestName { get; set; }
        public int? Mark { get; set; }
        public int? AmountOfFaithfull { get; set; }
        public int? AmountOfIncorrect { get; set; }
    }
}
