using ProStep.DataTransferObject.Quizzes.Answer;
using ProStep.SharedKernel.Enums.Quizzes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Quizzes.Question
{
    public class GetQuestionDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool? Answer { get; set; } // if type == TrueOrFalse 
        public QuestionType Type { get; set; }
        public List<GetAnswerDto> Answers { get; set; }
    }
}
