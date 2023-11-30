using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Courses.Comment
{
    public class AddCommentDto
    {
        public string Text { get; set; }
        public Guid? MainCommentId { get; set; }
        public Guid VideoId { get; set; }
    }
}
