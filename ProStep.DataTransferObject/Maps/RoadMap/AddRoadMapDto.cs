using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Maps.RoadMap
{
    public class AddRoadMapDto
    {
        public string Title { get; set; }

        public Guid CategoryId { get; set; }
        public List<SubCategory> SubCategories { get; set; }
    }

    public class SubCategory
    {
        public Guid CategoryId { get; set; }
        public List<Guid> SubCategories { get; set; }
    }
}
