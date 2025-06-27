using Microsoft.AspNetCore.Identity;

namespace AccountManagement.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}