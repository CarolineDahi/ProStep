using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Profile.Portfolio
{
    public class UpdateDto
    {
        public List<Guid>? Ids { get; set; }
        public List<Guid>? RemoveIds { get; set; }
    }
}
