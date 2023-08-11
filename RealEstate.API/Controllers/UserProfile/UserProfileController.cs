using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.API.Helpers;
using RealEstate.Application.Contracts.UserProfile;
using RealEstate.DTO.WriteOnly.AuthDTO;
using System.Net;

namespace RealEstate.API.Controllers.UserProfile
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserProfileController : BaseController
    {
        private readonly IUserProfile _userProfile;

        public UserProfileController(IUserProfile userProfile)
        {
            _userProfile = userProfile;
        }

        /// <summary>
        /// This is for the user to upload their info
        /// </summary>
        /// <param name="userProfileDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update_user_profile")]
        [ProducesResponseType(typeof(JsonMessage<string>), 200)]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UserProfileDTO userProfileDTO)
        {
            string result = await _userProfile.CreateUserProfile(UserEmail, userProfileDTO);
            if (string.IsNullOrWhiteSpace(result))
            {
                return Ok(new JsonMessage<string>()
                {
                    status = true,
                    success_message = "User Profile Updated",
                    status_code = 200

                });
            }
            return Ok(new JsonMessage<string>()
            {
                status = false,
                error_message = result,
                status_code = (int)HttpStatusCode.NotFound
            });

        }

        /// <summary>
        /// This is for the user to upload their logo
        /// </summary>
        /// <param name="imageDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update_profile_photo")]
        [ProducesResponseType(typeof(JsonMessage<string>), 200)]
        public async Task<IActionResult> UploadProfilePhoto([FromForm] UserPhotoDTO logoDTO)
        {
            string result = await _userProfile.UploadLogo(UserEmail, logoDTO);
            if (string.IsNullOrWhiteSpace(result))
            {
                return Ok(new JsonMessage<string>()
                {
                    status = true,
                    success_message = "Logo has been Updated",
                    status_code = 200

                });
            }
            return Ok(new JsonMessage<string>()
            {
                status = false,
                error_message = result,
                status_code = (int)HttpStatusCode.NotFound
            });
        }
    }
}
