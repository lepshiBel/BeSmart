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

        // в зависимости от значения верности ответа  увеличивает кол-во  верных/неверных ответов в статусТеста
        public async Task<StatusTest> CheckAnswerAndUpdateStatusTest(bool fidelity, int statusTestId)
        {
            var updated = await repoManager.StatusTest.UpdateAnswerFieldInStatusTestAsync(fidelity, statusTestId);

            if (updated == null) return null;

            return updated;
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
