using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Maps.RoadMap
{
    public class GetRoadMapDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
