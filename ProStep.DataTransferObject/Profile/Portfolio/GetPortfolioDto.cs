using ProStep.DataTransferObject.General.Category;
using ProStep.DataTransferObject.Profile.Education;
using ProStep.DataTransferObject.Profile.WorkExperience;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Profile.Portfolio
{
    public class GetPortfolioDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsCoach { get; set; }
        public string? Bio { get; set; }
        public string? ImagePath { get; set; }
        public string? CoverPath { get; set; }
        public List<GetCategoryDto> Careers { get; set; }
        public List<GetCategoryDto> Skills { get; set; }
        public IEnumerable<GetWorkDto> WorkExperiences { get; set; }
        public IEnumerable<GetEducationDto> Educations { get; set; }
    }
}
