using BeSmart.Domain.DTOs.StatusTheme;
using BeSmart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeSmart.Domain.DTOs.Membership
{
    public class MembershipWithThemesDTO
    {
        public int Id { get; set; }
        public string? MembershipStatus { get; set; }
        public string? CourseName { get; set; }
        public int CourseCountThemes { get; set; }
        public List<StatusThemeWithThemeDTO> StatusThemes { get; set; }
    }
}
