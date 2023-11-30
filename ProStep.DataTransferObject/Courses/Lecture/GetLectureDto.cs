using ProStep.DataTransferObject.Shared.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Courses.Lecture
{
    public class GetLectureDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public GetFileDto Video { get; set; }
    }
}
