using ProStep.Model.Base;
using ProStep.Model.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.TrainingProjects
{
    public class ToDoFile : BaseEntity
    {
        public ToDoFile() { }

        public Guid ToDoId { get; set; }
        public ToDo ToDo { get; set; }

        public Guid FileId { get; set; }
        public _File File { get; set; }
    }
}
