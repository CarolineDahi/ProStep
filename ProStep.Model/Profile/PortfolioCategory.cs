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
    public class PortfolioCategory : BaseEntity
    {
        public PortfolioCategory() { }

        public SKillType Type { get; set; }

        public Guid PortfolioId { get; set; }
        public Portfolio Portfolio { get; set;}

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
