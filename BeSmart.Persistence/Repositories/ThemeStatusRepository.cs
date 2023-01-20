﻿using BeSmart.Domain;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence.Data;

namespace BeSmart.Persistence.Repositories
{
    public class ThemeStatusRepository : RepositoryBase<StatusTheme, BeSmartDbContext>, IStatusThemeRepository
    {
        public ThemeStatusRepository(BeSmartDbContext context) : base(context) { }
    }
}