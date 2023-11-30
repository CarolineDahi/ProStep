using ProStep.DataTransferObject.General.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Maps.RoadMap
{
    public class DetailsRoadMapDto : GetRoadMapDto
    {
        public List<GetSubCategoryDto> Categories { get; set; }
    }

    public class GetSubCategoryDto : GetCategoryDto
    {
        public List<GetCategoryDto> SubCategories { get; set; }
    }
}
