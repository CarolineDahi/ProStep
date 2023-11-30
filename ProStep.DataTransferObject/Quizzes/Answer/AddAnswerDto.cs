using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Quizzes.Answer
{
    public class AddAnswerDto
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}
