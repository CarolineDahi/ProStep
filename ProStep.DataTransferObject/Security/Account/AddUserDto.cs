using ProStep.SharedKernel.Enums.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Security.Account
{
    public class AddUserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public UserType UserType { get; set; }
        public string? DeviceToken { get; set; }
    }
}
