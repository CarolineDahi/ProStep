using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProStep.Base;
using ProStep.DataSourse.Context;
using ProStep.DataTransferObject.Security.Account;
using ProStep.Model.Profile;
using ProStep.Model.Security;
using ProStep.SharedKernel.Enums;
using ProStep.SharedKernel.Enums.Security;
using ProStep.SharedKernel.ExtensionMethods;
using ProStep.SharedKernel.OperationResult;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Security.SecurityRepository
{
    public partial class AccountRepository : ProStepRepository, IAccountRepository
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IConfiguration configuration;

        public AccountRepository(ProStepDBContext context, UserManager<User> userManager,
                                    SignInManager<User> signInManager, IConfiguration configuration) : base(context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        public async Task<OperationResult<IEnumerable<GetUserDto>>> GetAll()
        {
            var users = await context.Users.Select(u => new GetUserDto
            {
                Id = u.Id,
                UserName = u.UserName,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                UserType = u.UserType,
                IsApproved = u.Portfolio!.IsApprove,
            }).ToListAsync();

            return OperationR.SetSuccess(users.AsEnumerable());
        }

        public async Task<OperationResult<IEnumerable<GetUserDto>>> GetByType(UserType type)
        {
            var users = await context.Users.Where(u => u.UserType.Equals(type))
                                           .Select(u => new GetUserDto
                                           {
                                               Id = u.Id,
                                               FirstName = u.FirstName,
                                               LastName = u.LastName,
                                               Email = u.Email,
                                               PhoneNumber = u.PhoneNumber,
                                               IsApproved = u.Portfolio!.IsApprove
                                           }).ToListAsync();

            return OperationR.SetSuccess(users.AsEnumerable());
        }

        public async Task<OperationResult<GetUserDetailsDto>> Login(LoginDto loginDto)
        {
            var user = await context.Users.Where(u => u.UserName == loginDto.UserName).Include(u => u.Portfolio).FirstOrDefaultAsync();
            
            if (user is null)
            {
                return OperationR.SetFailed<GetUserDetailsDto>($"{loginDto.UserName} Not Found", OperationResultType.Forbidden);
            }
            var loginResult = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);


            if (loginResult == SignInResult.Success)
            {
                user.DeviceToken = loginDto.DeviceToken;
                var account = await FillAccount(user, true);

                AssignRefreshTokenIfRememberMe(loginDto.RememberMe, account);

                return OperationR.SetSuccess(account);
            }
            else
            {
                return OperationR.SetFailed<GetUserDetailsDto>("UnCorrect Password", OperationResultType.Forbidden);
            }
        }

        public async Task<OperationResult<TokenDto>> RefreshToken(Guid id, string refreshToken)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user is null)
                return OperationR.SetFailed<TokenDto>("user not found.",OperationResultType.Forbidden);

            if (!user.PasswordHash.Equals(refreshToken))
                return OperationR.SetFailed<TokenDto>("Invalid Refresh Token", OperationResultType.Forbidden);

            var roles = await userManager.GetRolesAsync(user);
            var expierDate = DateTime.Now.AddMinutes(GlobalValue.DefaultExpireTokenMinut);

            return OperationR.SetSuccess<TokenDto>(new TokenDto()
            {
                Token = GenerateJwtToken(user, roles, expierDate),
                RefreshToken = user.PasswordHash,
            });
        }

        public async Task<OperationResult<GetUserDetailsDto>> Create(AddUserDto userDto)
        {
            var user = new User
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                UserType = userDto.UserType,
                GenerationStamp = ""
            };

            var identityResult = await userManager.CreateAsync(user, userDto.Password);

            if (!identityResult.Succeeded)
            {
                return OperationR.SetFailed<GetUserDetailsDto>(String.Join(",", identityResult.Errors.Select(error => error.Description)));
            }
            user.DeviceToken = userDto.DeviceToken;

            var Roles = new List<string> {};

            if(user.UserType is UserType.Coach || user.UserType is UserType.User)  
                Roles.Add(UserType.User.ToString());

            if (user.UserType is UserType.Admin)
            {
                Roles.Add(UserType.DashUser.ToString());
                Roles.Add(UserType.Admin.ToString());
            }

            IdentityResult roleIdentityResult = await userManager.AddToRolesAsync(user, Roles);

            if (!roleIdentityResult.Succeeded)
            {
                return OperationR.SetFailed<GetUserDetailsDto>(String.Join(",", roleIdentityResult.Errors.Select(error => error.Description)));
            }

            if (user.UserType is UserType.Coach || user.UserType is UserType.User)
            {
                var portfolio = new Portfolio
                {
                    UserId = user.Id,
                    IsApprove = null
                };
                context.Add(portfolio);
                user.PortfolioId = portfolio.Id;
            }
            
            await context.SaveChangesAsync();

            return OperationR.SetSuccess(await FillAccount(user, true));
        }

        public async Task<OperationResult<bool>> ChangePassword(ChangePasswordDto dto)
        {
            var user = await context.Users
                                    .Where(u => u.Id.Equals(context.CurrentUserId()))
                                    .SingleOrDefaultAsync();
            if (user is not null)
            {
                var result = await userManager.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword);
                if (result == IdentityResult.Success)
                {
                    await context.SaveChangesAsync();
                    return OperationR.SetSuccess(true); 
                }
                else
                {
                    return OperationR.SetSuccess(false);
                }
            }
            else
            {
                return OperationR.SetFailed<bool>("User not found.", OperationResultType.NotExist);
            }
        }

        //public async Task<OperationResult<bool>> ForgetPassword(string email)
        //{
        //    var user = await userManager.FindByEmailAsync(email);
            
        //    if (user is not null)
        //    {
        //        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        //        //ToDo : 
        //        if (true)//await SendResetPasswordToken(userDto, token))
        //        {
        //            return OperationR.SetSuccess(true);
        //        }
        //        else
        //        {
        //            return OperationR.SetSuccess(false);
        //        }
        //    }
        //    else
        //    {
        //        return OperationR.SetFailed<bool>("User not found.");
        //    }
        //}

        public async Task<OperationResult<bool>> Delete(IEnumerable<Guid> ids)
        {
            var users = await context.Users
                                     .Where(u => ids.Contains(u.Id))
                                     .Include(u => u.Comments)
                                     .Include(u => u.BootcampUsers)
                                     .Include(u => u.CourseUsers)
                                     .Include(u => u.Portfolio)
                                     .Include(u => u.UserQuesAnswers)
                                     .Include(u => u.UserFiles)
                                     .Include(u => u.UserConversations)
                                     .Include(u => u.TrainingProjects)
                                     .Include(u => u.Courses)
                                     .Include(u => u.NotificationUsers)
                                     .Include(u => u.ProjectUsers)
                                     .Include(u => u.TaskUsers)
                                     .Include(u => u.ToDoUsers)
                                     .ToListAsync();
            foreach (var user in users)
            {
                if(user.UserType == UserType.Coach || user.UserType == UserType.User) 
                    context.Remove(user.Portfolio);
                context.RemoveRange(user.Comments);
                context.RemoveRange(user.BootcampUsers);
                context.RemoveRange(user.CourseUsers);
                context.RemoveRange(user.UserQuesAnswers);
                context.RemoveRange(user.UserFiles);
                context.RemoveRange(user.UserConversations);
                context.RemoveRange(user.TrainingProjects);
                context.RemoveRange(user.Courses);
                context.RemoveRange(user.NotificationUsers);
                context.RemoveRange(user.ProjectUsers);
                context.RemoveRange(user.TaskUsers);
                context.RemoveRange(user.ToDoUsers);
                context.Remove(user);
            }
            await context.SaveChangesAsync();
            return OperationR.SetSuccess(true);
        }

        public async Task<OperationResult<bool>> ApprovedAccount(Guid? userId, bool IsApproved)
        {
            var user = await context.Users.Where(user => user.Id == userId)
                                          .Include(user => user.Portfolio)
                                          .FirstOrDefaultAsync();
            user.Portfolio.IsApprove = IsApproved;
            if(IsApproved)
            {
                user.UserType = UserType.Coach;
                await userManager.AddToRoleAsync(user, ProStepRole.ActiveCoach.ToString());
            }
            else
            {
                user.UserType = UserType.User;
                await userManager.RemoveFromRoleAsync(user, ProStepRole.ActiveCoach.ToString());
            }
            context.Update(user);
            await context.SaveChangesAsync();
            return OperationR.SetSuccess(true);
        }
    }
}
