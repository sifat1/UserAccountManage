using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

[Authorize(Roles = "Admin")]
public class ManageRolesModel : PageModel
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public ManageRolesModel(RoleManager<IdentityRole> roleManager) => _roleManager = roleManager;

    [BindProperty]
    public string RoleName { get; set; }

    public List<IdentityRole> Roles { get; set; }

    public void OnGet()
    {
        Roles = _roleManager.Roles.ToList();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!string.IsNullOrWhiteSpace(RoleName))
        {
            if (!await _roleManager.RoleExistsAsync(RoleName))
                await _roleManager.CreateAsync(new IdentityRole(RoleName));
        }
        return RedirectToPage();
    }
}