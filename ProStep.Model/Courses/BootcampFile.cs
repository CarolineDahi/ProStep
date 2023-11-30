using ProStep.Model.Base;
using ProStep.Model.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.Courses
{
    public class BootcampFile : BaseEntity
    {
        public BootcampFile()
        {
        }

        public Guid BootcampId { get; set; }
        public Bootcamp Bootcamp { get; set; }

        public Guid FileId { get; set; }
        public _File File { get; set; }
    }
}
