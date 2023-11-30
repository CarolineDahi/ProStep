using ProStep.DataTransferObject.Security.Account;
using ProStep.SharedKernel.Enums.Security;
using ProStep.SharedKernel.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Security.SecurityRepository
{
    public interface IAccountRepository
    {
        Task<OperationResult<IEnumerable<GetUserDto>>> GetAll();
        Task<OperationResult<IEnumerable<GetUserDto>>> GetByType(UserType type);
        Task<OperationResult<GetUserDetailsDto>> Login(LoginDto loginDto);
        Task<OperationResult<TokenDto>> RefreshToken(Guid id, string refreshToken);
        Task<OperationResult<GetUserDetailsDto>> Create(AddUserDto userDto);
        Task<OperationResult<bool>> ChangePassword(ChangePasswordDto dto);
        //Task<OperationResult<bool>> ForgetPassword(string email);
        Task<OperationResult<bool>> Delete(IEnumerable<Guid> ids);
        Task<OperationResult<bool>> ApprovedAccount(Guid? userId, bool IsApproved);
    }
}
