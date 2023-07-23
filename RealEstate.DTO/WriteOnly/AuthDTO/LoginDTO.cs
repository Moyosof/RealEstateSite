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
    public class LoginDTO
    {
        [JsonProperty("Email")]
        [Required]
        public string Email { get; set; }
        [JsonProperty("Password")]
        [PasswordPropertyText(true)]
        [Required]
        public string Password { get; set; }
    }
}
