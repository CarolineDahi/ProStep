using ProStep.Model.Base;
using ProStep.Model.Security;
using ProStep.SharedKernel.Enums.Portfolio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.Profile
{
    public class Community : BaseEntity
    {
        public Community() { }

        public CommunityType Type { get; set; } 

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid ConnectorId { get; set; }
        public User Connector { get; set; }
    }
}
