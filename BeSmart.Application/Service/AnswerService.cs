using AutoMapper;
using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs.Answer;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Service
{
    public class AnswerService : IServiceAnswer
    {
        private readonly IRepositoryManager repoManager;
        private readonly IMapper mapper;

        public AnswerService(IRepositoryManager repoManager, IMapper mapper)
        {
            this.repoManager = repoManager;
            this.mapper = mapper;
        }

        public async Task<List<AnswerDTO>>? GetAllAnswersAsync()
        {
            var answers = await repoManager.Answer.GetAllAsync();
            return answers == null ? null : mapper.Map<List<AnswerDTO>>(answers);
        }

        public async Task<AnswerDTO>? FindAnswerByIdAsync(int id)
        {
            var answer = await repoManager.Answer.GetAsync(id);
            return answer == null ? null : mapper.Map<AnswerDTO>(answer);
        }

        public async Task<AnswerDTO> AddAnswerAsync(AnswerCreationDTO answerDto)
        {
            var answerToCreate = mapper.Map<Answer>(answerDto);
            var createdAnswer = await repoManager.Answer.AddAsync(answerToCreate);
            return mapper.Map<AnswerDTO>(createdAnswer);
        }

        public async Task<AnswerDTO> UpdateAnswerAsync(int id, AnswerUpdateDTO answerUpdateDto)
        {
            var answerToUpdate = mapper.Map<Answer>(answerUpdateDto);
            var updated = await repoManager.Answer.UpdateAsync(id, answerToUpdate);
            return updated == null ? null : mapper.Map<AnswerDTO>(updated);
        }

        public async Task<Answer> DeleteAnswerAsync(int id)
        {
            return await repoManager.Answer.DeleteAsync(id);
        }
    }
}
