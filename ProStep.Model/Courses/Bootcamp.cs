using ProStep.Model.Base;
using ProStep.Model.TrainingProjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.Courses
{
    public class Bootcamp : BaseEntity
    {
        public Bootcamp() 
        {
            BootcampCourses = new HashSet<BootcampCourse>();
            BootcampCategories = new HashSet<BootcampCategory>();
            BootcampUsers = new HashSet<BootcampUser>();
            TrainingProjects = new HashSet<TrainingProject>();
            BootcampFiles = new HashSet<BootcampFile>();
        }

        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<BootcampCourse> BootcampCourses { get; set; } 
        public ICollection<BootcampCategory> BootcampCategories { get; set; }
        public ICollection<BootcampUser> BootcampUsers { get; set; }
        public ICollection<TrainingProject> TrainingProjects { get; set; }
        public ICollection<BootcampFile> BootcampFiles { get; set; }
    }
}
