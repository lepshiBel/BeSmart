using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeSmart.Domain.DTOs
{
    public class TestWithQuestionsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<AnswerDTO> Answers { get; set; }
    }
}
