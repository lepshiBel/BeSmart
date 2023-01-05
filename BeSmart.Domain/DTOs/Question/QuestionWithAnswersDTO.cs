using BeSmart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeSmart.Domain.DTOs.Question
{
    public class QuestionWithAnswersDTO
    {
        public int Id { get; set; } 
        public string Text { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
