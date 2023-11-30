using ProStep.Model.Base;
using ProStep.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.Quizzes
{
    public class UserQuesAnswer : BaseEntity
    {
        public UserQuesAnswer() { }

        public Guid? AnswerId { get; set; }
        public Answer Answer { get; set; }

        public Guid QuestionId { get; set; }
        public Question Question { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
