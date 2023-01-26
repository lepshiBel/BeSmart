﻿using BeSmart.Domain;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence.Data;

namespace BeSmart.Persistence.Repositories
{
    public class TestStatusRepository : RepositoryBase<StatusTest, BeSmartDbContext>, IStatusTestRepository
    {
        public TestStatusRepository(BeSmartDbContext context) : base(context) { }
    }
}
