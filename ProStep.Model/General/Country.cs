using ProStep.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.General
{
    public class Country : BaseEntity
    {
        public Country()
        {
            Cities = new HashSet<City>();
        }

        public string Name { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}
