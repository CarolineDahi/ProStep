using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Profile.Portfolio
{
    public class GetMyPortfolioDto : GetPortfolioDto
    {
        public string Email { get; set; }
        public Guid? CityId { get; set; }
        public string? CityName { get; set; }
        public Guid? CountryId { get; set; }
        public string? CountryName { get; set; }
    }
}
