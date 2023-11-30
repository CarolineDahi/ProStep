using ProStep.Model.Base;
using ProStep.SharedKernel.Enums.Quizzes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.Quizzes
{
    public class Question : BaseEntity
    {
        public Question() 
        {
            UserQuesAnswers = new HashSet<UserQuesAnswer>();
            Answers = new HashSet<Answer>();
        }

        public string Text { get; set; }
        public bool? Answer { get; set; } // if type == TrueOrFalse 
        public QuestionType Type { get; set; }

        public Guid QuizId { get; set; }
        public Quiz Quiz { get; set; }

        public ICollection<UserQuesAnswer> UserQuesAnswers { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
