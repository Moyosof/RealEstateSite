using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.DTO.WriteOnly.AuthDTO
{
    public class ResetPasswordDTO
    {
        [Required]
        [JsonProperty("Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [JsonProperty("token")]
        public string token { get; set; }

        [Required]
        [PasswordPropertyText(true)]
        [JsonProperty("NewPassword")]
        public string NewPassword { get; set; }

        [Required]
        [PasswordPropertyText(true)]
        [JsonProperty("ConfirmNewPassword")]
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
    }
}
