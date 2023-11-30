using ProStep.DataTransferObject.General.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.General.Country
{
    public class GetCountryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<GetBaseCityDto> CityDtos { get; set; }
    }
}
