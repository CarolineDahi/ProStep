using ProStep.DataTransferObject.Courses.Section;
using ProStep.DataTransferObject.General.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Courses.Course
{
    public class DetailsCourseDto : GetCourseDto
    {
        public string Description { get; set; }
        public DateTime LastUpdated { get; set; }
        public double? Evaluation { get; set; }
        public List<string> Targets { get; set; }
        public List<string> Requirements { get; set; }
        public List<GetCategoryDto> SubCategories { get; set; }
        public List<GetCategoryDto> MainCategories { get; set; }
        public List<GetSectionDto> Sections { get; set; }
        public bool? IsFavourite { get; set; }
        // ToDo : Add Packages + Categories + Bootcamps
    }
}
