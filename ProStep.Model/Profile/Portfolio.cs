using ProStep.Model.Base;
using ProStep.Model.General;
using ProStep.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.Profile
{
    public class Portfolio : BaseEntity
    {
        public Portfolio() 
        {
            PortfolioCategories = new HashSet<PortfolioCategory>();
            Educations = new HashSet<Education>();
            WorkExperiences = new HashSet<WorkExperience>();
        }
        public string? Bio { get; set; }
        public bool? IsApprove { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid? CityId { get; set; }
        public City City { get; set; }

        public ICollection<PortfolioCategory> PortfolioCategories { get; set; }
        public ICollection<Education> Educations { get; set; }
        public ICollection<WorkExperience> WorkExperiences { get; set; }
    }
}
