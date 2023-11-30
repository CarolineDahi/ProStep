using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.General.City
{
    public class GetCityDto : GetBaseCityDto
    {
        public Guid CountryId { get; set; }
        public string CountryName { get; set; }
    }
}
