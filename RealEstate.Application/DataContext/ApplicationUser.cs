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
        public string WhatsAppNumber { get; set; }
        public string Logo { get; set; }
        public string OfficeState { get; set; }
        public string OfficeLGA { get; set; }
        public string OfficeAddress { get; set; }
        public string OrganizationDescription { get; set; }
        public string OrganizationServices { get; set; }
        public string BusinessCategory { get; set; }
        public string SocialMedia { get; set; }

    }
}
