using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Quizzes.Quiz
{
    public class QuizDto
    {
        public Guid QuizId { get; set; }
        public List<QuizQuestionDto> Questions { get; set; }
    }

    public class QuizQuestionDto
    {
        public Guid QuestionId { get; set; }
        public Guid AnswerId { get; set; }
    }
}
