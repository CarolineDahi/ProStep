using ProStep.Model.Base;
using ProStep.Model.Courses;
using ProStep.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.TrainingProjects
{
    public class TrainingProject : BaseEntity
    {
        public TrainingProject() 
        {
            ProjectUsers = new HashSet<ProjectUser>();
            Tasks = new HashSet<Task>();
        }

        public string Description { get; set; }

        public Guid BootcampId { get; set; }
        public Bootcamp Bootcamp { get; set; }

        public Guid CoachId { get; set; }
        public User Coach { get; set; }

        public ICollection<ProjectUser> ProjectUsers { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
