using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Profile.Education
{
    public class GetEducationDto : EducationDto
    {
        public Guid Id { get; set; }
        public string FacultyName { get; set; }
        public Guid UniversityId { get; set; }
        public string UniversityName { get; set; }
    }
}
