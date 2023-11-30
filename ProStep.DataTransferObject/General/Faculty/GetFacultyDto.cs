using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.General.Faculty
{
    public class GetFacultyDto : GetBaseFacultyDto
    {
        public Guid UniversityId { get; set; }
        public string UniversityName { get; set; }
    }
}
