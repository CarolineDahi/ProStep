using ProStep.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.Courses
{
    public class Lecture : BaseEntity
    {
        public Lecture() 
        {
            Comments = new HashSet<Comment>();
            LectureFiles = new HashSet<LectureFile>();
            LectureUsers = new HashSet<LectureCourseUser>();
        }
        public string Title { get; set; }
        public string Description { get; set; }

        public Guid SectionId { get; set; }
        public Section Section { get; set; }

        public ICollection<Comment> Comments { get; set; } 
        public ICollection<LectureFile> LectureFiles { get; set; }
        public ICollection<LectureCourseUser> LectureUsers { get; set; }
    }
}
