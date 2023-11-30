using Microsoft.IdentityModel.Tokens;
using ProStep.DataTransferObject.Security.Account;
using ProStep.Model.Security;
using ProStep.SharedKernel.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Security.SecurityRepository
{
    public partial class AccountRepository
    {
        private async Task<GetUserDetailsDto> FillAccount(User user, bool isNewGeneration = false)
        {
            var roles = await userManager.GetRolesAsync(user);
            var expierDate = DateTime.Now.AddMinutes(GlobalValue.DefaultExpireTokenMinut);

            if (isNewGeneration)
            {
                user.GenerationStamp = Guid.NewGuid().ToString();
                await userManager.UpdateAsync(user);
            }

            var account = new GetUserDetailsDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserType = user.UserType,
                //CityId = user.Portfolio?.CityId!,
                //CityName = user.Portfolio?.City!.Name,
                //CountryName = user.Portfolio?.City!.Country.Name, 
                DateCreated = user.DateCreated,
                ImagePath = user.ImagePath,
                Token = GenerateJwtToken(user, roles, expierDate),
                RefreshToken = user.PasswordHash,
                IsApproved = user.Portfolio?.IsApprove
            };

            return account;
        }

        private void AssignRefreshTokenIfRememberMe(bool rememberMe, GetUserDetailsDto userDto) =>
            _ = !rememberMe ? userDto.RefreshToken = string.Empty : string.Empty;

        private string GenerateJwtToken(User user, IList<string> roles, DateTime expierDate)
        {
            var claims = new List<Claim>
            {
                //new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("generate-date", DateTime.Now.ToLocalTime().ToString()),
                new Claim("generation-stamp", user.GenerationStamp),
                //new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                //new Claim(ClaimTypes.Name, user.UserName),
                new Claim("Type", user.UserType.ToString()),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:secret"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token =  new JwtSecurityToken(configuration["Jwt:validIssuer"],
                                             configuration["Jwt:validAudience"],
                                             claims,
                                             expires: expierDate,
                                             signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
