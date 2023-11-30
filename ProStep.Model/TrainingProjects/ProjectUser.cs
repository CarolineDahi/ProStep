using ProStep.Model.Base;
using ProStep.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.TrainingProjects
{
    public class ProjectUser : BaseEntity
    {
        public ProjectUser()
        {
        }

        public bool IsDone { get; set; } 
        public bool IsFavourite { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid ProjectId { get; set; }
        public TrainingProject Project { get; set; }

    }
}
