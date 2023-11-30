using ProStep.Model.Base;
using ProStep.Model.Shared;
using ProStep.SharedKernel.Enums.Portfolio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.Security
{
    public class UserFile : BaseEntity
    {
        public UserFile()
        {
        }

        public UserFileType Type { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid FileId { get; set; }
        public _File File { get; set; }
    }
}
