using ProStep.Model.Base;
using ProStep.Model.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.Courses
{
    public class LectureFile : BaseEntity
    {
        public LectureFile() { }

        public Guid LectureId { get; set; }
        public Lecture Lecture { get; set; }

        public Guid FileId { get; set; }
        public _File File { get; set; }
    }
}
