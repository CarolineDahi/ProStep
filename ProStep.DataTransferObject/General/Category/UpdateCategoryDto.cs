using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.General.Category
{
    public class UpdateCategoryDto
    {
        public Guid Id { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public string Name { get; set; }
    }
}
