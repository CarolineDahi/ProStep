using ProStep.SharedKernel.Enums.Portfolio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Profile.WorkExperience
{
    public class WorkDto 
    {
        public Guid? Id { get; set; }
        public string Position { get; set; }
        public string CompanyName { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public WorkType WorkType { get; set; }
        public Guid CityId { get; set; }
    }
}
