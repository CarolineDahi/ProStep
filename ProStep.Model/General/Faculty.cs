using ProStep.Model.Base;
using ProStep.Model.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.General
{
    public class Faculty : BaseEntity
    {
        public Faculty() 
        {
            Educations = new HashSet<Education>();
        }

        public string Name { get; set; }

        public Guid UniversityId { get; set; }
        public University University { get; set; }

        public ICollection<Education> Educations { get; set; }
    }
}
