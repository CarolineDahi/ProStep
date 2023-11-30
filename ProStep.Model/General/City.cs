using ProStep.Model.Base;
using ProStep.Model.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.General
{
    public class City : BaseEntity
    {
        public City()
        {
            Portfolios = new HashSet<Portfolio>();
        }

        public string Name { get; set; }

        public Guid CountryId { get; set; }
        public Country Country { get; set; }

        public ICollection<Portfolio> Portfolios { get; set; }
    }
}
