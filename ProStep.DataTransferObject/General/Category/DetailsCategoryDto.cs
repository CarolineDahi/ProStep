using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.General.Category
{
    public class DetailsCategoryDto : GetCategoryDto
    {
        public IEnumerable<GetCategoryDto> Childs { get; set; }
    }
}
