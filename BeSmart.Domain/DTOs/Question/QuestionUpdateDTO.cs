using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeSmart.Domain.DTOs.Question
{
    public class QuestionUpdateDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int TestId { get; set; }
    }
}
