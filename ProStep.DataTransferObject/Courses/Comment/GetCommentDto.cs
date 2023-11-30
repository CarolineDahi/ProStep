using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Courses.Comment
{
    public class GetCommentDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string ImagePath { get; set; }
        public int NumOfReply { get; set; }
        public bool IsUpdated { get; set; }
    }
}
