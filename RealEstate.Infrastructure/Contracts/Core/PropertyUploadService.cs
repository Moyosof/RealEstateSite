using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.VisualBasic;
using RealEstate.Application.Contracts.Core;
using RealEstate.DataAccess.UnitOfWork.Interface;
using RealEstate.Domain.Entities.Core;
using RealEstate.Domain.ReadOnly;
using RealEstate.DTO.WriteOnly.CoreDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Infrastructure.Contracts.Core
{
    public class PropertyUploadService : IPropertyUpload
    {
        private readonly IUnitOfWork<PropertyUpload> _unitOfWork;
        private readonly IFileService _fileService;

        public PropertyUploadService(IUnitOfWork<PropertyUpload> unitOfWork, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }

        public async Task<string> DestroyProperty(Guid id)
        {
            var property = await _unitOfWork.Repository.ReadSingle(id);
            if(property == null)
            {
                return "Property not found";
            }
            try
            {
                await _unitOfWork.Repository.Delete(id);
                await _unitOfWork.SaveAsync();
                
                if (!string.IsNullOrEmpty(property.ImageUpload))
                {
                    string deleteImage = await _fileService.DeleteImage(property.ImageUpload);
                    property.ImageUpload = deleteImage;
                }
                return "Property deleted successfully";
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<List<PropertyUploadDto>> GetAllProperty(CancellationToken cancellationToken)
        {
            var property = await _unitOfWork.Repository.ReadAllQuery().Select(x => new PropertyUpload
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Purpose = x.Purpose,
                PropertyType = x.PropertyType,
                NumbersOfRoom = x.NumbersOfRoom,
                NumbersOfBathroom= x.NumbersOfBathroom,
                NumbersOfToilet = x.NumbersOfToilet,
                Furnished = x.Furnished,
                Serviced =  x.Serviced,
                NewlyBuilt = x.NewlyBuilt,
                State = x.State,
                Locality = x.Locality,
                Area= x.Area,
                Amount= x.Amount,
                InstallmentalPayment= x.InstallmentalPayment,
                ImageUpload= x.ImageUpload,
            }).ToListAsync(cancellationToken);

            List<PropertyUploadDto> result = new List<PropertyUploadDto>();
            foreach (var items in property)
            {
                result.Add(new PropertyUploadDto(items));
            }
            return result;
        }

        public async Task<PropertyUploadDto> GetProperty(Guid id)
        {
            var property = await _unitOfWork.Repository.ReadSingle(id);
            PropertyUploadDto propertyUpload = new PropertyUploadDto(property);
            return propertyUpload;
        }

        public async Task<PropertyUploadDto> UpdateProperty(PropertyUploadDTO propertyUpload, Guid id)
        { 

            var property = await _unitOfWork.Repository.ReadSingle(id);
            if(id != property.Id)
            {
                return null;
            }
            
            try
            {
                property.Title = propertyUpload.Title;
                property.Description = propertyUpload.Description;
                property.Purpose = propertyUpload.Purpose;
                property.PropertyType = propertyUpload.PropertyType;
                property.NumbersOfRoom = propertyUpload.NumbersOfRoom;
                property.NumbersOfBathroom= propertyUpload.NumbersOfBathroom;
                property.NumbersOfToilet = propertyUpload.NumbersOfToilet;
                property.Furnished= propertyUpload.Furnished;
                property.Serviced= propertyUpload.Serviced;
                property.NewlyBuilt= propertyUpload.NewlyBuilt;
                property.State= propertyUpload.State;
                propertyUpload.Locality= propertyUpload.Locality;
                property.Area= propertyUpload.Area;
                property.Amount= propertyUpload.Amount;
                property.InstallmentalPayment= propertyUpload.InstallmentalPayment;

                if (propertyUpload.ImageUpload != null)
                {
                    string imageUrl = await _fileService.CreateImage(propertyUpload.ImageUpload);
                    property.ImageUpload = imageUrl;
                }

                _unitOfWork.Repository.Update(property);
                await _unitOfWork.SaveAsync();
                PropertyUploadDto properties = new PropertyUploadDto(property);
                return properties;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<string> UploadProperty(PropertyUploadDTO propertyUpload)
        {
            string imagePath = null;
            if(propertyUpload.ImageUpload != null)
            {
                try
                {
                    imagePath = await _fileService.CreateImage(propertyUpload.ImageUpload);

                }
                catch(Exception ex)
                {
                    return ex.Message;
                }
            }
            PropertyUpload property = new PropertyUpload()
            {
                Title = propertyUpload.Title,
                Description = propertyUpload.Description,
                Purpose = propertyUpload.Purpose,
                PropertyType = propertyUpload.PropertyType,
                NumbersOfRoom = propertyUpload.NumbersOfRoom,
                NumbersOfBathroom = propertyUpload.NumbersOfBathroom,
                NumbersOfToilet = propertyUpload.NumbersOfToilet,
                Furnished = propertyUpload.Furnished,
                Serviced = propertyUpload.Serviced,
                NewlyBuilt = propertyUpload.NewlyBuilt,
                State = propertyUpload.State,
                Locality = propertyUpload.Locality,
                Area = propertyUpload.Area,
                Amount = propertyUpload.Amount,
                InstallmentalPayment = propertyUpload.InstallmentalPayment,
                ImageUpload = imagePath
            };
            try
            {
                await _unitOfWork.Repository.Add(property);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return string.Empty;
        }
    }
}
