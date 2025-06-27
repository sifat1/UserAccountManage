using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AccountingSystem.Models;
using Microsoft.AspNetCore.Authorization;

[Authorize(Roles = "Admin")]
public class ManageUsersModel : PageModel
{
    public List<ApplicationUser> Users { get; set; }
    public UserManager<ApplicationUser> UserManager { get; }
    public RoleManager<IdentityRole> RoleManager { get; }

    public ManageUsersModel(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        UserManager = userManager;
        RoleManager = roleManager;
    }

    public void OnGet()
    {
        Users = UserManager.Users.ToList();
    }

    public async Task<IActionResult> OnPostAsync(string userId, string role)
    {
        var user = await UserManager.FindByIdAsync(userId);
        if (user != null && await RoleManager.RoleExistsAsync(role))
        {
            var roles = await UserManager.GetRolesAsync(user);
            await UserManager.RemoveFromRolesAsync(user, roles);
            await UserManager.AddToRoleAsync(user, role);
        }
        return RedirectToPage();
    }
}