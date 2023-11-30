using ProStep.Model.Base;
using ProStep.Model.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.Maps
{
    public class RoadMapCategory : BaseEntity
    {
        public Guid RoadMapId { get; set; }
        public RoadMap RoadMap { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public Guid? RoadMapCategoryParentId { get; set; }
        [ForeignKey(nameof(RoadMapCategoryParentId))]
        public RoadMapCategory? RoadMapCategoryParent { get; set; }

        [InverseProperty(nameof(RoadMapCategoryParent))]
        public ICollection<RoadMapCategory> RoadMapCategories { get; set;}
    }
}
