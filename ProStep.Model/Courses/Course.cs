using ProStep.Model.Base;
using ProStep.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.Courses
{
    public class Course : BaseEntity
    {
        public Course() 
        {
            Sections = new HashSet<Section>();
            CourseCategories = new HashSet<CourseCategory>();
            BootcampCourses = new HashSet<BootcampCourse>();
            CourseUsers = new HashSet<CourseUser>();
            CourseFiles = new HashSet<CourseFile>();
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public string Target { get; set; }

        public Guid CoachId { get; set; }
        public User Coach { get; set; }

        public ICollection<Section> Sections { get; set; } 
        public ICollection<CourseCategory> CourseCategories { get; set; }
        public ICollection<BootcampCourse> BootcampCourses { get; set; }    
        public ICollection<CourseFile> CourseFiles { get; set; }
        public ICollection<CourseUser> CourseUsers { get; set; }
    }
}
