using Microsoft.AspNetCore.Http;
using ProStep.DataTransferObject.Profile.Education;
using ProStep.DataTransferObject.Profile.WorkExperience;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Profile.Portfolio
{
    public class UpdatePortfolioDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid CityId { get; set; }
        public string Bio { get; set; }
        public bool ApproveToCoach { get; set; }
        public List<EducationDto> Educations { get; set; }
        public List<Guid> EducationRemoveIds { get; set; }
        public List<WorkDto> WorkExperiences { get; set; }
        public List<Guid> WorkRemoveIds { get; set; }
    }
}
