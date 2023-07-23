using RealEstate.Application.DataContext;
using RealEstate.DTO.WriteOnly.AuthDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Contracts.Auth
{
    public interface IUserAuth
    {
        Task<string> RegisterUser(RegisterUserDTO registerUserDTO);
        Task<string> RegisterAdmin(RegisterUserDTO adminDTO);
        Task<string> LoginUser(LoginDTO loginDTO);
        Task<List<string>> GetUserRoles(string username);
        Task<ApplicationUser> FindByUserName(string userName);

        Task<string> ChangePassword(string userEmail, ChangePasswordDTO changePasswordDTO);
        Task<string> PasswordResetToken(ApplicationUser user);
        Task<string> ResetPassword(ApplicationUser user, string token, string newPassword);
        Task<string> CreateRoleAndAddUserToRole(string name, ApplicationUser user);

    }
}
