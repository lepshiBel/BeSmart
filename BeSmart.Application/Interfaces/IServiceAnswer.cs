﻿using BeSmart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeSmart.Application.Interfaces
{
    public interface IServiceAnswer
    {
        Task<List<Answer>> GetAllAnswersAsync();
        Task<Answer> FindAnswerByIdAsync(int id);
        Task<Answer> AddAnswerAsync(Answer answer);
        Task<Answer> UpdateAnswerAsync(Answer answer);
        Task<Answer> DeleteAnswerAsync(int id);
    }
}
