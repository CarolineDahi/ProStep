using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Profile.Portfolio
{
    public class UpdateCertificateDto
    {
        public IEnumerable<IFormFile>? Files { get; set; }
        public IEnumerable<Guid>? RemoveIds { get; set; }
    }
}
