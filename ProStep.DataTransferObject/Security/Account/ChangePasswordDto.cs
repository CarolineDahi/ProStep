using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Security.Account
{
    public class ChangePasswordDto
    {
        public string Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
