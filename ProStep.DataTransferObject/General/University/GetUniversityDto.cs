using ProStep.DataTransferObject.General.Faculty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.General.University
{
    public class GetUniversityDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<GetBaseFacultyDto> FacultyDtos { get; set; }
    }
}
