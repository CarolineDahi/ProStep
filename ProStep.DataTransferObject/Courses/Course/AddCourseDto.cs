using Microsoft.AspNetCore.Http;
using ProStep.DataTransferObject.Courses.Section;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Courses.Course
{
    public class AddCourseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public IEnumerable<Guid> CategoryIds { get; set; }
        public List<string> Targets { get; set; }
        public List<string> Requirements { get; set; }
        public List<AddSectionDto> Sections { get; set; }
    }
}
