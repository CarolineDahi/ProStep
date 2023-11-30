using ProStep.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.General
{
    public class University : BaseEntity
    {
        public University() 
        {
            Faculties = new HashSet<Faculty>();
        }

        public string Name { get; set; }

        public ICollection<Faculty> Faculties { get; set; }
    }
}
