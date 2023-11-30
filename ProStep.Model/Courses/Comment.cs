using ProStep.Model.Base;
using ProStep.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.Courses
{
    public class Comment : BaseEntity
    {
        public Comment() 
        {
            SubComments = new HashSet<Comment>();
        }

        public string Text { get; set; }

        public Guid? MainCommentId { get; set; }
        public Comment? MainComment { get; set; }

        public Guid VideoId { get; set; }
        public Lecture Video { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public ICollection<Comment> SubComments { get; set;}
    }
}
