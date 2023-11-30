using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Courses.Course
{
    public class GetCourseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public  Guid CoachId { get; set; }
        public string CoachName { get; set; }
        public string ImagePath { get; set; }
        public double Evaluation { get; set; }
        public int? NumOfViews { get; set; }
    }
}
