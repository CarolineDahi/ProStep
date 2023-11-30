using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.General.Category
{
    public class GetCategoryDashDto : GetCategoryDto
    {
        public IEnumerable<GetCategoryDto> SubCategories { get; set; }
    }
}
