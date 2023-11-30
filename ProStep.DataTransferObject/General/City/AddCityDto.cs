using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.General.City
{
    public class AddCityDto
    {
        public string Name { get; set; }
        public Guid CountryId { get; set; }
    }
}
