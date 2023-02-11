using AutoMapper;
using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs.StatusTest;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Service
{
    public class StatusTestService : IServiceStatusTest
    {
        private readonly IRepositoryManager repositoryManger;
        private readonly IMapper mapper;

        public StatusTestService(IRepositoryManager repositoryManger, IMapper mapper)
        {
            this.repositoryManger = repositoryManger;
            this.mapper = mapper;
        }

        // подсчет оценки если кол-во верных ответов + кол-во неверных == общему кол-ву вопросов 
        public async Task<StatusTestDTO> FihishTheTestAsync(int statusTestId)
        {
            var statusTest = await repositoryManger.StatusTest.GetStatusTestWithTest(statusTestId);

            if (statusTest == null) return null;

            int mark = CalculateTheMark(statusTest.Test.QuestionsCount, statusTest.AmountOfFaithfullAnswers, statusTest.AmountOfIncorrectAnswers);

            if (mark == -1) return mapper.Map<StatusTestDTO>(statusTest);

            statusTest.Mark = mark;
            var updated = await repositoryManger.StatusTest.UpdateStatusAndMarkInStatusTestAsync(statusTest);
            return mapper.Map<StatusTestDTO>(updated);
        }

        public async Task<StatusTest> StartTestAsync(int testId, int statusThemeId)
        {
            var createdStatusTest = await repositoryManger.StatusTest.AddStatusTestAsync(testId, statusThemeId);
            return createdStatusTest;
        }

        public int CalculateTheMark(int total, int? faithfull, int? incorrect)
        {
            decimal faithfullAnswers = (decimal)faithfull,
                totalAmount = total;

            if (faithfull + incorrect != totalAmount) return -1;

            var mark = Math.Ceiling(faithfullAnswers / totalAmount * 10);
            return Convert.ToInt32(mark);
        }

        public async Task<StatusTest> FihishTheAttemptAsync(int statusTestId)
        {
            var deleted = await repositoryManger.StatusTest.DeleteAsync(statusTestId);

            if (deleted == null) return null;

            return deleted;
        }
    }
}
