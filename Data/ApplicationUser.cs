using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using VSHCTwebApp.Components.Models;

namespace VSHCTwebApp.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public List<UserCompetence> UserCompetences { get; set; } = new();
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Group { get; set; }
    }

}
