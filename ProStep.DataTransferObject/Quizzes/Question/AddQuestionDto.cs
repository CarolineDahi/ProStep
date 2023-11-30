using ProStep.DataTransferObject.Quizzes.Answer;
using ProStep.SharedKernel.Enums.Quizzes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Quizzes.Question
{
    public class AddQuestionDto 
    {
        public string Text { get; set; }
        public bool? Answer { get; set; } // if type == TrueOrFalse 
        public QuestionType Type { get; set; }
        public List<AddAnswerDto>? Answers { get; set; }
    }
}
