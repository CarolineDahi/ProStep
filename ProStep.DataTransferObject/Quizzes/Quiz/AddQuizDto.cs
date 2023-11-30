using ProStep.DataTransferObject.Quizzes.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Quizzes.Quiz
{
    public class AddQuizDto
    {
        public List<AddQuestionDto> Questions { get; set; }
    }
}
