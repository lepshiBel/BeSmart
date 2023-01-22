using BeSmart.Application.Interfaces;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeSmart.Application.Service
{
    public class StatusTestService : IServiceStatusTest
    {
        private readonly IRepositoryManager repositoryManger;

        public StatusTestService(IRepositoryManager repositoryManger)
        {
            this.repositoryManger = repositoryManger;
        }

        public async Task<StatusTest> AddStatusTestAsync(int testId, int statusThemeId)
        {
            var createdStatusTest = await repositoryManger.StatusTest.AddStatusTestAsync(testId, statusThemeId);
            return createdStatusTest;
        }
    }
}
