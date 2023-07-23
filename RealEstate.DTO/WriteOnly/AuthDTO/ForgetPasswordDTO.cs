using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.DTO.WriteOnly.AuthDTO
{
    public class ForgetPasswordDTO
    {

        [JsonProperty("Email")]
        [EmailAddress]
        [Required]
        public string Email { get; set; }


        [JsonProperty("ResetPasswordPageLink")]
        [Url]
        public string ResetPasswordPageLink { get; set; }
    }
}
