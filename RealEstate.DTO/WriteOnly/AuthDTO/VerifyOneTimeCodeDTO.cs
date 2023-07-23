using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.DTO.WriteOnly.AuthDTO
{
    public class VerifyOneTimeCodeDTO
    {
        [Required]
        [JsonProperty("Token")]
        public string Token { get; set; }

        [Required]
        [JsonProperty("Sender")]
        public string Sender { get; set; }
    }
}
