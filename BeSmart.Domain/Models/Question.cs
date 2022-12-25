﻿using BeSmart.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeSmart.Domain.Models
{
    public class Question : EntityBase
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int TestId { get; set; }
        public List<Answer>? Answers { get; set; }
    }
}