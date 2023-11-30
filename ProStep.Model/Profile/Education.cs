using ProStep.Model.Base;
using ProStep.Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.Profile
{
    public class Education : BaseEntity
    {
        public Education() { }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }

        public Guid PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; }

        public Guid FacultyId { get; set; }
        public Faculty Faculty { get; set; }
    }
}
