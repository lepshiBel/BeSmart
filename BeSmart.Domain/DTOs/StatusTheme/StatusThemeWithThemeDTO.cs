using BeSmart.Domain.DTOs.Theme;

namespace BeSmart.Domain.DTOs.StatusTheme
{
    public class StatusThemeWithThemeDTO
    {
        public int Id { get; set; }
        public string? Status { get; set; }
        public ThemeDTO Theme { get; set; }
    }
}
