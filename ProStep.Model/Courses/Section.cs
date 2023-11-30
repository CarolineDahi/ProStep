using ProStep.Model.Base;
using ProStep.Model.Quizzes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.Courses
{
    public class Section : BaseEntity
    {
        public Section() 
        {
            Lectures = new HashSet<Lecture>();
        }
        public string Title { get; set; }
        public string? Description { get; set; }

        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public Guid? QuizId { get; set; }
        public Quiz Quiz { get; set; }

        public ICollection<Lecture> Lectures { get; set; }
    }
}
