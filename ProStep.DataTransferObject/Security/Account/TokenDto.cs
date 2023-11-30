using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Security.Account
{
    public class TokenDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
