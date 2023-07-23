using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace RealEstate.Application.DataContext
{
    /// <summary>
    /// This class inherits from the IDentityUser Class
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string State { get; set; }

    }
}
