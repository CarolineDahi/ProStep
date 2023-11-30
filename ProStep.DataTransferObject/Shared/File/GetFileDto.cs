using ProStep.SharedKernel.Enums.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Shared.File
{
    public class GetFileDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public FileType Type { get; set; }
    }
}
