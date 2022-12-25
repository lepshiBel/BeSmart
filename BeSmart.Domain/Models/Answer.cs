using BeSmart.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeSmart.Domain.Models
{
    public class Answer : EntityBase
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool Fidelity { get; set; }
        public Question Question { get; set; }
        public int QuestionId { get; set; }
    }
}