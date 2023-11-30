using ProStep.DataTransferObject.Courses.Lecture;
using ProStep.DataTransferObject.Quizzes.Quiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Courses.Section
{
    public class GetSectionDto 
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<GetLectureDto> Lectures { get; set; }
        public Guid? QuizId { get; set; }
    }
}
