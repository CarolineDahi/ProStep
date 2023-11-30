using ProStep.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.Courses
{
    public class LectureCourseUser : BaseEntity
    {
        public LectureCourseUser() { }

        public Guid LectureId { get; set; }
        public Lecture Lecture { get; set; }

        public Guid CourseUserId { get; set; }
        public CourseUser CourseUser { get; set; }
    }
}
