using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Profile.Education
{
    public class EducationDto
    {
        public Guid? Id { get; set; }
        public Guid FacultyId { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
    }
}
