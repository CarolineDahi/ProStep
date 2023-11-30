using ProStep.SharedKernel.Enums.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.Security.Account
{
    public class GetUserDetailsDto : TokenDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        //public Guid? CityId { get; set; }
        //public string? CityName { get; set; }
        //public string? CountryName { get; set; }
        public UserType UserType { get; set; }
        public DateTime DateCreated { get; set; }
        public string ImagePath { get; set; }
        public bool? IsApproved { get; set; }

    }
}
