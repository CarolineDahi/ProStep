using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Courses.Bootcamp
{
    public class UpdateBootcampDto : AddBootcampDto
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; } // for delete old image
    }
}
