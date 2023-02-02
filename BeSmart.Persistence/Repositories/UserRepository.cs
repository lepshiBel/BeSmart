﻿using BeSmart.Domain;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence.Data;

namespace BeSmart.Persistence.Repositories
{
    public class UserRepository : RepositoryBase<User, BeSmartDbContext>, IUserRepository
    {
        public UserRepository(BeSmartDbContext dbContext)
        : base(dbContext)
        {
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = context.Users.SingleOrDefault(u => u.Email == email);
            return user;
        }
    }
}
