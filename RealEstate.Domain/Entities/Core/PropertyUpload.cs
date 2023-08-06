using RealEstate.Domain.ReadOnly;
using RealEstate.DTO.WriteOnly.CoreDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Domain.Entities.Core
{
    public class PropertyUpload
    {
        public PropertyUpload()
        {

        }
        public PropertyUpload(PropertyUploadDto propertyUpload)
        {
            Id = Guid.NewGuid();
            Title = propertyUpload.Title;
            Description = propertyUpload.Description;
            Purpose = propertyUpload.Purpose;
            PropertyType = propertyUpload.PropertyType;
            NumbersOfRoom = propertyUpload.NumbersOfRoom;
            NumbersOfBathroom = propertyUpload.NumbersOfBathroom;
            NumbersOfToilet = propertyUpload.NumbersOfToilet;
            Furnished = propertyUpload.Furnished;
            Serviced = propertyUpload.Serviced;
            NewlyBuilt = propertyUpload.NewlyBuilt;
            State = propertyUpload.State;
            Locality = propertyUpload.Locality;
            Area = propertyUpload.Area;
            Amount = propertyUpload.Amount;
            InstallmentalPayment = propertyUpload.InstallmentalPayment;
            ImageUpload = propertyUpload.ImageUpload;

        }
        public PropertyUpload(PropertyUploadDTO propertyUpload)
        {
            Id = Guid.NewGuid();
            Title = propertyUpload.Title;
            Description = propertyUpload.Description;
            Purpose = propertyUpload.Purpose;
            PropertyType = propertyUpload.PropertyType;
            NumbersOfRoom = propertyUpload.NumbersOfRoom;
            NumbersOfBathroom = propertyUpload.NumbersOfBathroom;
            NumbersOfToilet = propertyUpload.NumbersOfToilet;
            Furnished = propertyUpload.Furnished;
            Serviced = propertyUpload.Serviced;
            NewlyBuilt = propertyUpload.NewlyBuilt;
            State = propertyUpload.State;
            Locality = propertyUpload.Locality;
            Area = propertyUpload.Area;
            Amount = propertyUpload.Amount;
            InstallmentalPayment = propertyUpload.InstallmentalPayment;
            ImageUpload = propertyUpload.ImageUpload.FileName;
        }

        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Purpose { get; set; }
        public string PropertyType { get; set; }
        public string NumbersOfRoom { get; set; }
        public string NumbersOfBathroom { get; set; }
        public string NumbersOfToilet { get; set; }
        public string Furnished { get; set; }
        public string Serviced { get; set; }
        public string NewlyBuilt { get; set; }

        public string State { get; set; }
        public string Locality { get; set; }
        public string Area { get; set; }

        public string Amount { get; set; }

        public string InstallmentalPayment { get; set;}
        public string ImageUpload { get; set;}


    }
}
