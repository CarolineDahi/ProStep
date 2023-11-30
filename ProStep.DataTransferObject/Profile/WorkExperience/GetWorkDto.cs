using ProStep.SharedKernel.Enums.Portfolio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Profile.WorkExperience
{
    public class GetWorkDto : WorkDto
    {
        public Guid Id { get; set; }
        public string CityName { get; set; }
        public Guid CountryId { get; set; }
        public string CountryName { get; set; }
    }
}
