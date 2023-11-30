using ProStep.Model.Base;
using ProStep.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.Courses
{
    public class BootcampUser : BaseEntity
    {
        public BootcampUser()
        {
        }
        public DateTime? DateFinished { get; set; }
        public bool IsFavourite { get; set; }
        public double? Evaluation { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid BootcampId { get; set; }
        public Bootcamp Bootcamp { get; set; }
    }
}
