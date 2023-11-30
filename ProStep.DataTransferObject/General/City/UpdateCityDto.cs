using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.General.City
{
    public class UpdateCityDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CountryId { get; set; }
    }
}
