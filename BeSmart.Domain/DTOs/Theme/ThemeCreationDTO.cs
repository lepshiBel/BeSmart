using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeSmart.Domain.DTOs.Theme
{
    public class ThemeCreationDTO
    {
        public string Name { get; set; }

        public int CountLesson { get; set; }

        public int CountTest { get; set; }

        public int CourseId { get; set; }
    }
}
