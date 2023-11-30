using ProStep.DataTransferObject.Quizzes.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Quizzes.Quiz
{
    public class GetQuizDto
    {
        public Guid Id { get; set; }
        public List<GetQuestionDto> Quiz { get; set; }
    }
}
