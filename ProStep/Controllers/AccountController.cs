using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProStep.Base;
using ProStep.DataTransferObject.Security.Account;
using ProStep.General.CityRepository;
using ProStep.Profile.PortfolioRepository;
using ProStep.Security.SecurityRepository;
using ProStep.SharedKernel.Enums.Security;
using ProStep.SharedKernel.ExtensionMethods;
using ProStep.SharedKernel.OperationResult;
using System.ComponentModel.DataAnnotations;

namespace ProStep.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        [HttpGet(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.Admin)]
        [ProducesResponseType(typeof(IEnumerable<GetUserDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
            => await accountRepository.GetAll().ToJsonResultAsync();


        [HttpGet(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser, ProStepRole.User)]
        [ProducesResponseType(typeof(List<GetUserDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByType([Required] UserType type)
            => await accountRepository.GetByType(type).ToJsonResultAsync();

        [HttpPost(GlobalValue.RouteBoth)]
        [ProducesResponseType(typeof(GetUserDetailsDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(LoginDto loginDto)
            => await accountRepository.Login(loginDto).ToJsonResultAsync();

        [HttpPost(GlobalValue.RouteBoth)]
        [AllowAnonymous]
        [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> RefreshToken(Guid id, string refreshToken)
        => await accountRepository.RefreshToken(id, refreshToken).ToJsonResultAsync();

        [HttpPost(GlobalValue.RouteBoth)]
        [ProducesResponseType(typeof(GetUserDetailsDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(AddUserDto userDto)
            => await accountRepository.Create(userDto).ToJsonResultAsync();

        [HttpPost(GlobalValue.RouteBoth)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
            => await accountRepository.ChangePassword(dto).ToJsonResultAsync();

        //[HttpPost(GlobalValue.RouteBoth)]
        //[ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        //public async Task<IActionResult> ForgetPassword(string email)
        //    => await accountRepository.ForgetPassword(email).ToJsonResultAsync();

        [HttpDelete(GlobalValue.RouteBoth)]
        [ProStepAuthorize(ProStepRole.SuperAdmin)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([Required] Guid id)
            => await accountRepository.Delete(new List<Guid> { id }).ToJsonResultAsync();

        [HttpDelete(GlobalValue.RouteBoth)]
        [ProStepAuthorize(ProStepRole.SuperAdmin)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteRange(IEnumerable<Guid> ids)
            => await accountRepository.Delete(ids).ToJsonResultAsync();

        [HttpGet(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> ApprovedAccount(Guid? userId, bool IsApproved)
            => await accountRepository.ApprovedAccount(userId, IsApproved).ToJsonResultAsync();
    }
}
