using BeSmart.Domain.DTOs.Theme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeSmart.Domain.DTOs.StatusTheme
{
    public class StatusThemeWithThemeDTO
    {
        public int Id { get; set; }
        public string? Status { get; set; }
        public ThemeDTO Theme { get; set; }
    }
}
