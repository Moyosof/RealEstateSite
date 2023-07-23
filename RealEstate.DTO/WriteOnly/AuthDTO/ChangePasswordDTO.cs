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
    public class ChangePasswordDTO
    {
        [Required]
        [PasswordPropertyText(true)]
        [JsonProperty("CurrentPassword")]
        public string CurrentPassword { get; set; }

        [Required]
        [PasswordPropertyText(true)]
        [JsonProperty("NewPassword")]
        public string NewPassword { get; set; }

        [Required]
        [PasswordPropertyText(true)]
        [JsonProperty("ConfirmPassword")]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}
