using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Quizzes.Answer
{
    public class GetAnswerDto : AddAnswerDto
    {
        public Guid Id { get; set; }
    }
}
