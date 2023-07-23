using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using RealEstate.Application.Contracts.Auth;
using RealEstate.Application.DataContext;
using RealEstate.Domain.Entities.ReadOnly;
using RealEstate.Domain.Enum;
using RealEstate.DTO.WriteOnly.AuthDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Infrastructure.Contracts.Auth
{
    public class UserAuth : IUserAuth, ITokenAuth
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserAuth(
           UserManager<ApplicationUser> userManager,
           IUserStore<ApplicationUser> userStore,
           SignInManager<ApplicationUser> signInManager,
           RoleManager<IdentityRole> roleManager
            )
        {
            _userManager = userManager;
            _userStore = userStore;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public event Func<object, OneTimeCodeDTO, CancellationToken, Task> OneTimePasswordEmailEventhandler;

        public Task AddOneTimeCodeToSender(OneTimeCodeDTO oneTimeCodeDTO, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<string> ChangePassword(string userEmail, ChangePasswordDTO changePasswordDTO)
        {
            // Get Current User 
            var currentUser = await _userManager.FindByEmailAsync(userEmail);

            // Change User Password 
            var changedPassword = await _userManager.ChangePasswordAsync(currentUser, changePasswordDTO.CurrentPassword, changePasswordDTO.NewPassword);

            if (changedPassword.Succeeded)
            {
                return string.Empty;
            }

            else
            {
                string error = changedPassword.Errors.First().Description;

                if (error == "PasswordMismatch")
                {
                    return "Current password is incorrect";
                }
                else
                {
                    return "We are not able to change your password right now. Please contact admin";
                }
            }
        }

        public async Task<string> CreateRoleAndAddUserToRole(string name, ApplicationUser user)
        {
            bool checkedRole = await _roleManager.RoleExistsAsync(name);
            if (!checkedRole)
            {
                IdentityResult role = await _roleManager.CreateAsync(new IdentityRole(name));
                if (!role.Succeeded)
                {
                    return role.Errors.First().Description;
                }
            }

            IdentityResult AddUserToRole = await _userManager.AddToRoleAsync(user, name);
            if (AddUserToRole.Succeeded)
            {
                return string.Empty;
            }
            return AddUserToRole.Errors.First().Description;
        }

        public async Task<ApplicationUser> FindByUserName(string userName) => await _userManager.FindByNameAsync(userName);


        public async Task<List<string>> GetUserRoles(string username)
        {
            var User = await _userManager.FindByEmailAsync(username);

            if (User == null) return new();

            var roles = await _userManager.GetRolesAsync(User);

            return roles.ToList();
        }

        public async Task<string> LoginUser(LoginDTO loginDTO)
        {
            var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, false, lockoutOnFailure: false);


            if (result.Succeeded)
            {

                return string.Empty;
            }
            return "Invalid Login credentials";
        }

        public async Task<string> PasswordResetToken(ApplicationUser user) => await _userManager.GeneratePasswordResetTokenAsync(user);


        public async Task<string> RegisterAdmin(RegisterUserDTO adminDTO)
        {
            return await Register(adminDTO, Roles.Admin);
        }

        public async Task<string> RegisterUser(RegisterUserDTO registerUserDTO)
        {
            return await Register(registerUserDTO, Roles.User);
        }

        public async Task<string> ResetPassword(ApplicationUser user, string token, string newPassword)
        {
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            if (result.Succeeded)
            {
                return string.Empty;
            }
            return result.Errors.First().Description;
        }

        public Task<string> VerifyOneTimeCode(VerifyOneTimeCodeDTO verifyOneTimeCodeDTO)
        {
            throw new NotImplementedException();
        }

        private async Task<string> Register(RegisterUserDTO registerUserDTO, Roles role)
        {
            ApplicationUser user = new ApplicationUser();

            user = new ApplicationUser { Email = registerUserDTO.EmailAddress, PhoneNumber = registerUserDTO.PhoneNumber, State = registerUserDTO.State, PhoneNumberConfirmed = false, EmailConfirmed = false, UserName = registerUserDTO.EmailAddress, FirstName = registerUserDTO.FirstName.ToUpper(), LastName = registerUserDTO.LastName.ToUpper() };

            // Creates a new user and password hash
            var result = await _userManager.CreateAsync(user, registerUserDTO.Password);
            if (result.Succeeded)
            {
                string roleName = role.ToString();
                return await CreateRoleAndAddUserToRole(roleName, user);
            }
            return result.Errors.First().Description;
        }
    }
}
