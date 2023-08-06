using RealEstate.Domain.ReadOnly;
using RealEstate.DTO.WriteOnly.CoreDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Contracts.Core
{
    public interface IPropertyUpload
    {
        Task<List<PropertyUploadDto>> GetAllProperty(CancellationToken cancellationToken);
        Task<PropertyUploadDto> GetProperty(Guid id);
        Task<string> UploadProperty(PropertyUploadDTO propertyUpload);
        Task<PropertyUploadDto> UpdateProperty(PropertyUploadDTO propertyUpload, Guid id);
        Task<string> DestroyProperty(Guid id);

    }
}
