using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RealEstate.Application.Contracts.Core;
using RealEstate.Domain.Entities.Core.AuthUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Infrastructure.Contracts.Core
{
    public class FileService : IFileService
    {
        private readonly Cloudinary _cloudinary;
        private readonly CloudinarySettings _cloudinarySettings;
        private readonly ILogger<FileService> _logger;

        public FileService(IOptions<CloudinarySettings> cloudinarySettings, ILogger<FileService> logger)
        {
            _cloudinarySettings = cloudinarySettings.Value;
            _logger = logger;
            Account account = new Account
            (
                _cloudinarySettings.CloudName,
                _cloudinarySettings.ApiKey,
                _cloudinarySettings.ApiSecret
            );
            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> CreateImage(IFormFile img)
        {
            if (img == null || img.Length == 0)
            {
                throw new ArgumentException("Invalid image file.");
            }

            try
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(img.FileName, img.OpenReadStream()),
                    Transformation = new Transformation().Height(1250).Width(1870).Crop("fill")
                };
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                string url = uploadResult.SecureUrl.ToString();
                return url;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during file upload");
                throw;
            }
        }

        public async Task<string> DeleteImage(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException("Invalid url");
            }
            var publicId = GetPublicIdFromCloudinaryUrl(url);

            try
            {
                var deletionParams = new DeletionParams(publicId)
                {
                    ResourceType = ResourceType.Image
                };

                var deleteResult = await _cloudinary.DestroyAsync(deletionParams);
                return deleteResult.Result == "ok" ? "Image deleted Successfully." : "Image deletion failed.";
            }
            catch(Exception)
            {
                throw;
            }
        }
        private string GetPublicIdFromCloudinaryUrl(string url)
        {
            var uri = new Uri(url);
            var segments = uri.Segments;
            var publicId = segments.LastOrDefault()?.Split('.').FirstOrDefault();
            return publicId;
        }
    }
}
