using ProStep.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.Quizzes
{
    public class Answer : BaseEntity
    {
        public Answer() 
        {
            UserQuesAnswers = new HashSet<UserQuesAnswer>();
        }

        public string Text { get; set; }
        public bool IsCorrect { get; set; }

        public Guid QuestionId { get; set; }
        public Question Question { get; set; }

        public ICollection<UserQuesAnswer> UserQuesAnswers { get; set; }
    }
}
