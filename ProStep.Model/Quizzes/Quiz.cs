using ProStep.Model.Base;
using ProStep.Model.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.Quizzes
{
    public class Quiz : BaseEntity
    {
        public Quiz() 
        {
            Questions = new HashSet<Question>();
        }

        public int Timer { get; set; } // in munites

        public Guid SectionId { get; set; }
        public Section Section { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
