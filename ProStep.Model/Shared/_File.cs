using ProStep.Model.Base;
using ProStep.Model.Courses;
using ProStep.Model.Profile;
using ProStep.Model.TrainingProjects;
using ProStep.SharedKernel.Enums.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProStep.Model.Shared
{
    public class _File : BaseEntity
    {
        public _File() 
        {
            MessageFiles = new HashSet<MessageFile>();
            BootcampFiles = new HashSet<BootcampFile>();
            CourseFiles = new HashSet<CourseFile>();
            VideoFiles = new HashSet<LectureFile>();
            ToDoFiles = new HashSet<ToDoFile>();
        }

        public string Path { get; set; }
        public string Name { get; set; }
        public FileType Type { get; set; }

        public ICollection<LectureFile> VideoFiles { get; set; }
        public ICollection<BootcampFile> BootcampFiles { get; set; }
        public ICollection<CourseFile> CourseFiles { get; set; }
        public ICollection<MessageFile> MessageFiles { get; set; }
        public ICollection<ToDoFile> ToDoFiles { get; set; }
    }
}
