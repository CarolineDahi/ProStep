using ProStep.Model.Base;
using ProStep.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.TrainingProjects
{
    public class ToDoUser : BaseEntity
    {
        public ToDoUser()
        {
        }
        public DateTime? DateFinished { get; set; }
        public DateTime DeadLine { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid ToDoId { get; set; }
        public ToDo ToDo { get; set; }
    }
}
