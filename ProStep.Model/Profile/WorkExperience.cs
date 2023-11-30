using ProStep.Model.Base;
using ProStep.Model.General;
using ProStep.SharedKernel.Enums.Portfolio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.Profile
{
    public class WorkExperience : BaseEntity
    {
        public WorkExperience() { }
        public string Position { get; set; }
        public string CompanyName { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public WorkType WorkType { get; set; }

        public Guid PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; }

        public Guid CityId { get; set; }
        public City City { get; set; }
    }
}
