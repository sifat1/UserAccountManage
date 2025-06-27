using Microsoft.AspNetCore.Identity;

namespace AccountingSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}