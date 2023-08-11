using RealEstate.DTO.WriteOnly.AuthDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Contracts.UserProfile
{
    public interface IUserProfile
    {
        Task<string> CreateUserProfile(string userEmail, UserProfileDTO userProfileDTO);
        Task<string> UploadLogo(string userEmail, UserPhotoDTO logoDTO);

    }
}
