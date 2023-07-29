using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Contracts.Auth;
using RealEstate.Application.Contracts;
using RealEstate.Application.DataContext;
using RealEstate.API.Helpers;
using RealEstate.Application.Helpers;
using RealEstate.Domain.Entities.ReadOnly;
using RealEstate.DTO.WriteOnly.AuthDTO;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using RealEstate.DTO.OAuth;
using System.Security.Claims;

namespace RealEstate.API.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        private readonly IUserAuth _userAuth;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenAuth _tokenAuth;

        public AuthController(IUserAuth userAuth, ITokenGenerator tokenGenerator, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ITokenAuth tokenAuth)
        {
            _userAuth = userAuth;
            _tokenGenerator = tokenGenerator;
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenAuth = tokenAuth;
            _tokenAuth = tokenAuth;
        }

        /// <summary>
        /// This is to register a user
        /// </summary>
        /// <param name="registerUserDTO"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create_user")]
        [ProducesResponseType(typeof(JsonMessage<string>), 200)]
        public async Task<IActionResult> RegisterUser([FromBody]RegisterUserDTO registerUserDTO, CancellationToken cancellationToken)
        {
            #region Register User
            //Subcribe to Otp Email Event Handler
            _tokenAuth.OneTimePasswordEmailEventhandler += fluentEmail.SendOneTimeCodeEmail;

            var result = await _userAuth.RegisterUser(registerUserDTO);
            if (string.IsNullOrWhiteSpace(result))
            {
                string code = Util.GenerateRandomDigits(6); // generate 6 digit numbers
                OneTimeCodeDTO oneTimeCodeDTO = new OneTimeCodeDTO()
                {
                    Token = code,
                    Sender = registerUserDTO.EmailAddress
                };
                // Save token to the sender
                await _tokenAuth.AddOneTimeCodeToSender(oneTimeCodeDTO, cancellationToken);
                //unsubcribe event
                _tokenAuth.OneTimePasswordEmailEventhandler -= fluentEmail.SendOneTimeCodeEmail;
                return Ok(new JsonMessage<string>()
                {
                    status = true,
                    success_message = "Confirm Email to complete authentication"
                });
            }
            return Ok(new JsonMessage<string>()
            {
                error_message = result,
                status = false,
                status_code = (int)HttpStatusCode.BadRequest
            });
            #endregion
        }

        /// <summary>
        /// Enter the creadentials to create a new admin
        /// </summary>
        /// <param name="registerUserDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create_admin_user")]
        [ProducesResponseType(typeof(JsonMessage<string>), 200)]

        public async Task<IActionResult> RegisterAdmin(RegisterUserDTO registerUserDTO)
        {
            #region Register Owner
            var result = await _userAuth.RegisterAdmin(registerUserDTO);
            if (string.IsNullOrWhiteSpace(result))
            {
                return Ok(new JsonMessage<string>()
                {
                    status = true,
                    success_message = "Confirm Email to complete authentication"
                });
            }
            return Ok(new JsonMessage<string>()
            {
                error_message = result,
                status = false
            });
            #endregion
        }

        /// <summary>
        /// Enter the credentials to create a developer admin
        /// </summary>
        /// <param name="registerUserDTO"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("create_developer_user")]
        [ProducesResponseType(typeof(JsonMessage<string>), 200)]

        public async Task<IActionResult> RegisterDeveloper(RegisterUserDTO registerUserDTO, CancellationToken cancellation)
        {
            #region Register Developer
            _tokenAuth.OneTimePasswordEmailEventhandler += fluentEmail.SendOneTimeCodeEmail;

            var result = await _userAuth.RegisterDeveloper(registerUserDTO);
            if (string.IsNullOrWhiteSpace(result))
            {
                string code = Util.GenerateRandomDigits(6);
                OneTimeCodeDTO oneTime = new()
                {
                    Token= code,
                    Sender = registerUserDTO.EmailAddress
                };
                await _tokenAuth.AddOneTimeCodeToSender(oneTime, cancellation);
                _tokenAuth.OneTimePasswordEmailEventhandler -= fluentEmail.SendOneTimeCodeEmail;

                return Ok(new JsonMessage<string>()
                {
                    status = true,
                    success_message = "Confirm Email to complete authentication"
                });
            }
            return Ok(new JsonMessage<string>()
            {
                error_message = result,
                status = false,
                status_code = (int)HttpStatusCode.BadRequest
            });
            #endregion
        }

        /// <summary>
        /// Enter credentials to regsiter agent user
        /// </summary>
        /// <param name="registerUserDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create_agent_user")]
        [ProducesResponseType(typeof(JsonMessage<string>), 200)]

       
        public async Task<IActionResult> RegisterAgent(RegisterUserDTO registerUserDTO, CancellationToken cancellation)
        {
            _tokenAuth.OneTimePasswordEmailEventhandler += fluentEmail.SendOneTimeCodeEmail;

            #region Register Agent
            var result = await _userAuth.RegisterAgent(registerUserDTO);
            if (string.IsNullOrWhiteSpace(result))
            {
                string code = Util.GenerateRandomString(6);
                OneTimeCodeDTO oneTime = new()
                {
                    Token = code,
                    Sender = registerUserDTO.EmailAddress,
                };
                await _tokenAuth.AddOneTimeCodeToSender(oneTime, cancellation);
                _tokenAuth.OneTimePasswordEmailEventhandler -= fluentEmail.SendOneTimeCodeEmail;
                return Ok(new JsonMessage<string>()
                {
                    status = true,
                    success_message = "Confirm Email to complete authentication"
                });
            }
            return Ok(new JsonMessage<string>()
            {
                error_message = result,
                status = false,
                status_code = (int)HttpStatusCode.BadRequest
            });
            #endregion
        }


        /// <summary>
        /// The is to login any user
        /// </summary>
        /// <param name="loginDTO"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthResponse), 200)]
        [ProducesResponseType(typeof(JsonMessage<string>), 200)]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO, CancellationToken cancellationToken)
        {
            #region Login All User

            var result = await _userAuth.LoginUser(loginDTO);

            if (string.IsNullOrWhiteSpace(result))
            {
                ApplicationUser userInfo = await _userAuth.FindByUserName(loginDTO.Email);
                //  Verify if the eamil is confirmed
                if (userInfo.EmailConfirmed == false)
                {
                    //Subcribe to Otp Email Event Handler
                    _tokenAuth.OneTimePasswordEmailEventhandler += fluentEmail.SendOneTimeCodeEmail;

                    string response = "Email is not confirmed, an OTP has been sent to your email";

                    string code = Util.GenerateRandomDigits(6); // generate 6 digit numbers
                    OneTimeCodeDTO oneTimeCodeDTO = new OneTimeCodeDTO()
                    {
                        Token = code,
                        Sender = loginDTO.Email
                    };
                    // Save token to the sender
                    await _tokenAuth.AddOneTimeCodeToSender(oneTimeCodeDTO, cancellationToken);

                    // Unsubcribe the event
                    _tokenAuth.OneTimePasswordEmailEventhandler -= fluentEmail.SendOneTimeCodeEmail;

                    return Ok(new JsonMessage<string>()
                    {
                        status = false,
                        status_code = (int)HttpStatusCode.Accepted,
                        error_message = response,
                        is_verified = false
                    });

                }
                string fullName = $"{userInfo.LastName} {userInfo.FirstName}";
                //Generate Token
                var user_roles = await _userAuth.GetUserRoles(loginDTO.Email ?? string.Empty);
                var JwtToken = await _tokenGenerator.GenerateJwtToken(userInfo.Id, userInfo.PhoneNumber, userInfo.UserName, userInfo.Email, fullName ?? string.Empty, user_roles);
                return Ok(new JsonMessage<AuthResponse>()
                {
                    success_message = "Login Successful",
                    status = true,
                    status_code = 200,
                    result = new List<AuthResponse>()
                    {
                        JwtToken
                    },
                    is_verified = true
                });
            }
            return Ok(new JsonMessage<string>()
            {
                error_message = result,
                status = false
            });
            #endregion

        }


        /// <summary>
        /// This is to change password
        /// </summary>
        /// <param name="changePasswordDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("change_password")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthResponse), 200)]
        [ProducesResponseType(typeof(JsonMessage<string>), 200)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            #region Change Password
            //var currentUser = UserEmail;
            var currentUser = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;


            var result = await _userAuth.ChangePassword(currentUser, changePasswordDTO);
            if (string.IsNullOrWhiteSpace(result))
            {
                // Send Email password change successful
                await fluentEmail.SendChangePasswordEmail(currentUser);
                // Password changed succesfully
                return Ok(new JsonMessage<string>()
                {
                    status = true,
                    success_message = "Successfully Changed Password"
                });

            }
            return Ok(new JsonMessage<string>()
            {
                error_message = result,
                status = false
            });
            #endregion
        }


        /// <summary>
        /// Resend OTP
        /// </summary>
        /// <param name="email"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("resend_onetimecode")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(JsonMessage<string>), 200)]
        public async Task<IActionResult> ResendOneTimeCode([FromQuery] string email, CancellationToken cancellationToken)
        {
            //Subcribe to Otp Email Event Handler
            _tokenAuth.OneTimePasswordEmailEventhandler += fluentEmail.SendOneTimeCodeEmail;

            string code = Util.GenerateRandomDigits(6); // generate 6 digit numbers
            OneTimeCodeDTO oneTimeCodeDTO = new OneTimeCodeDTO()
            {
                Token = code,
                Sender = email
            };
            // Save token to the sender
            await _tokenAuth.AddOneTimeCodeToSender(oneTimeCodeDTO, cancellationToken);
            //unsubcribe event
            _tokenAuth.OneTimePasswordEmailEventhandler -= fluentEmail.SendOneTimeCodeEmail;

            return Ok(new JsonMessage<string>()
            {
                status = true,
                success_message = "One Time Code Resent",
                status_code = (int)HttpStatusCode.OK
            });
        }

        /// <summary>
        /// Verify The One Time Code sent to the User
        /// </summary>
        /// <param name="verifyOneTimeCodeDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("verify_Onetimecode")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(JsonMessage<string>), 200)]
        public async Task<IActionResult> VerifyOneTimeCode([FromBody] VerifyOneTimeCodeDTO verifyOneTimeCodeDTO)
        {
            string result = await _tokenAuth.VerifyOneTimeCode(verifyOneTimeCodeDTO);
            if (string.IsNullOrWhiteSpace(result))
            {

                return Ok(new JsonMessage<string>()
                {
                    status = true,
                    success_message = "User successfully Created",
                    status_code = (int)HttpStatusCode.OK
                });
            }
            return Ok(new JsonMessage<string>()
            {
                error_message = result,
                status = false,
                status_code = (int)HttpStatusCode.NotFound
            });
        }
    }
}
