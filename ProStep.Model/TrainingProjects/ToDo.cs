using ProStep.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.TrainingProjects
{
    public class ToDo : BaseEntity
    {
        public ToDo() { }

        public string Title { get; set; }

        public ICollection<ToDoFile> ToDoFiles { get; set; }
        public ICollection<ToDoUser> ToDoUsers { get; set; }
    }
}
