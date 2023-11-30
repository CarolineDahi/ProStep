using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.General.Faculty
{
    public class AddFacultyDto
    {
        public string Name { get; set; }
        public Guid UniversittyId { get; set; }
    }
}
