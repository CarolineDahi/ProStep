using ProStep.Model.Base;
using ProStep.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.Courses
{
    public class CourseUser : BaseEntity
    {
        public CourseUser()
        {
            VideoUsers = new HashSet<LectureCourseUser>();
        }
        public DateTime? DateFinished { get; set; }
        public bool IsFavourite { get; set; }
        public double? Evaluation { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public ICollection<LectureCourseUser> VideoUsers { get;}
    }
}
