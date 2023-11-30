using ProStep.Model.Base;
using ProStep.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProStep.Model.TrainingProjects;

namespace ProStep.Model.TrainingProjects
{
    public class TaskUser : BaseEntity
    {
        public TaskUser()
        {
        }
        public DateTime? DateFinished { get; set; }
        public DateTime DeadLine { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid TaskId { get; set; }
        public Task Task { get; set; }
    }
}
