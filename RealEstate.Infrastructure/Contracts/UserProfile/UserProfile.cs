using Microsoft.AspNetCore.Identity;
using RealEstate.Application.Contracts.Core;
using RealEstate.Application.Contracts.UserProfile;
using RealEstate.Application.DataContext;
using RealEstate.DTO.WriteOnly.AuthDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Infrastructure.Contracts.UserProfile
{
    public class UserProfile : IUserProfile
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFileService _fileService;

        public UserProfile(UserManager<ApplicationUser> userManager,
            IFileService fileService)
        {
            _userManager = userManager;
            _fileService = fileService;

        }

        public async Task<string> CreateUserProfile(string userEmail, UserProfileDTO userProfileDTO)
        {
            var currentUser = await _userManager.FindByEmailAsync(userEmail);

            if (currentUser != null)
            {
                currentUser.FirstName = userProfileDTO.FirstName;
                currentUser.LastName = userProfileDTO.LastName;
                currentUser.PhoneNumber = userProfileDTO.PhoneNumber;
                currentUser.WhatsAppNumber = userProfileDTO.WhatsAppNumber;
                currentUser.OfficeState = userProfileDTO.OfficeState;
                currentUser.OfficeLGA = userProfileDTO.OfficeLGA;
                currentUser.OfficeAddress = userProfileDTO.OfficeAddress;
                currentUser.OrganizationDescription = userProfileDTO.OrganizationDescription;
                currentUser.OrganizationServices = userProfileDTO.OrganizationServices;
                currentUser.BusinessCategory = userProfileDTO.BusinessCategory;
                currentUser.SocialMedia = userProfileDTO.SocialMedia;

                await _userManager.UpdateAsync(currentUser);
                return string.Empty;
            }
            return "User not found";
        }

        public async Task<string> UploadLogo(string userEmail, UserPhotoDTO logoDTO)
        {
            var currentUser = await _userManager.FindByEmailAsync(userEmail);

            if (currentUser is not null)
            {
                string imgPath = await _fileService.CreateImage(logoDTO.Logo);
                if (!string.IsNullOrWhiteSpace(imgPath))
                {
                    currentUser.Logo = imgPath;

                    await _userManager.UpdateAsync(currentUser);

                    return string.Empty;

                }
                return "Error uploading logo";

            }
            return "User not found.";
        }
    }
}
