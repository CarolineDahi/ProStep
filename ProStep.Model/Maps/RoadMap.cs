using ProStep.Model.Base;
using ProStep.Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.Maps
{
    public class RoadMap : BaseEntity
    {

        public string Title { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<RoadMapCategory>  RoadMapCategories { get; set; }
    }
}
