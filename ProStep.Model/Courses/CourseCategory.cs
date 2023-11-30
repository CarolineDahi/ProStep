using ProStep.Model.Base;
using ProStep.Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.Courses
{
    public class CourseCategory : BaseEntity
    {
        public CourseCategory() { }

        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
