using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.General.Statistics
{
    public class GetCategoryWithCountDto
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public int Value { get; set; }
    }
}
