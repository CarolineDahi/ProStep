using ProStep.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.TrainingProjects
{
    public class Task : BaseEntity
    {
        public Task() 
        { 
            ToDos = new HashSet<ToDo>(); 
            TaskUsers = new HashSet<TaskUser>();
        }

        public string Name { get; set; }
        public int DeadLine { get; set; } // in days

        public ICollection<ToDo> ToDos { get; set; }
        public ICollection<TaskUser> TaskUsers { get; set; }
    }
}
