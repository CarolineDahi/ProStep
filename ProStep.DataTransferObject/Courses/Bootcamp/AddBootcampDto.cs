using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Courses.Bootcamp
{
    public class AddBootcampDto   
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
        public IEnumerable<Guid> CourseIds { get; set; }
        public IEnumerable<Guid> CategoryIds { get; set; }
    }
}
