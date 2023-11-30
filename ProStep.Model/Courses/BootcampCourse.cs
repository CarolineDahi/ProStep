using ProStep.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.Courses
{
    public class BootcampCourse : BaseEntity
    {
        public BootcampCourse() { }

        public Guid BootcampId { get; set; }
        public Bootcamp Bootcamp { get; set; }

        public Guid CourseId { get; set; }
        public Course Course { get; set; }
    }
}
