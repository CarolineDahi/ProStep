using ProStep.DataTransferObject.Courses.Lecture;
using ProStep.DataTransferObject.Quizzes.Quiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Courses.Section
{
    public class AddSectionDto
    {
        public string Title { get; set; }
        public List<AddLectureDto> Lectures { get; set; }
        public AddQuizDto? Quiz { get; set; }
    }
}
