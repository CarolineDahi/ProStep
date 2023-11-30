using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Courses.Course
{
    public class AddEvaluateDto
    {
        public Guid CourseId { get; set; }
        public double Value { get; set; }
    }
}
