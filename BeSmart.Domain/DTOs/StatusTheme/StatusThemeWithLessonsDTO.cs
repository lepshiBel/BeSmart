using BeSmart.Domain.DTOs.StatusLesson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeSmart.Domain.DTOs.StatusTheme
{
    public class StatusThemeWithLessonsDTO
    {
        public int Id { get; set; }
        //public string? StatusTheme { get; set; }
        public string? NameOfTheme { get; set; }
        public int? CountOfLessons { get; set; }     
        public List<StatusLessonWithLessonDTO> StatusLessonsWithLessons { get; set; }
    }
}
