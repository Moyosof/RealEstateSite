using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.DTO.WriteOnly.AuthDTO
{
    public class UserProfileDTO
    {
        [JsonProperty("FirstName")]
        [Required]
        public string FirstName { get; set; }
        [JsonProperty("LastName")]
        [Required]
        public string LastName { get; set; }
        [JsonProperty("PhoneNumber")]
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [JsonProperty("State")]
        [Required]
        public string State { get; set; }
        [JsonProperty("WhatsAppNumber")]
        [Required]
        [Phone]
        public string WhatsAppNumber { get; set; }
        [JsonProperty("OfficeState")]
        [Required]
        public string OfficeState { get; set; }
        [JsonProperty("OfficeLGA")]
        [Required]
        public string OfficeLGA { get; set; }
        [JsonProperty("OfficeAddress")]
        [Required]
        public string OfficeAddress { get; set; }
        [JsonProperty("OrganizationDescription")]
        [Required]
        public string OrganizationDescription { get; set; }
        [JsonProperty("OrganizationServices")]
        [Required]
        public string OrganizationServices { get; set; }
        [JsonProperty("BusinessCategory")]
        [Required]
        public string BusinessCategory { get; set; }
        [JsonProperty("SocialMedia")]
        [Required]
        public string SocialMedia { get; set; }
    }
}
