using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Courses.Course
{
    public class UpdateCourseDto : AddCourseDto
    {
        public Guid Id { get; set; }
    }
}
