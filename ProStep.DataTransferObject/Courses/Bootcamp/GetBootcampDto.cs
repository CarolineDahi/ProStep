using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Courses.Bootcamp
{
    public class GetBootcampDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public double Evaluation { get; set; }
        public int? NumOfViews { get; set; }
    }
}
