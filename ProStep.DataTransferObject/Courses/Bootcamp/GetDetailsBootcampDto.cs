using ProStep.DataTransferObject.Courses.Course;
using ProStep.DataTransferObject.General.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Courses.Bootcamp
{
    public class GetDetailsBootcampDto : GetBootcampDto
    {
        public string Description { get; set; }
        public IEnumerable<GetCourseDto> CourseDtos { get; set; }
        public IEnumerable<GetCategoryDto> CategoryDtos { get; set; }
        public bool? IsFavourite { get; set; }
    }
}
