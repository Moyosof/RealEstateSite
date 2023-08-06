using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.DTO.WriteOnly.CoreDTO
{
    public class PropertyUploadDTO
    {
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
        public string InstallmentalPayment { get; set; }
        public IFormFile ImageUpload { get; set; }
    }
}
