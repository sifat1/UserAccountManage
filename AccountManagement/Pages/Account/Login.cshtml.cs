using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AccountingSystem.Models;

public class LoginModel : PageModel
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public LoginModel(SignInManager<ApplicationUser> signInManager) => _signInManager = signInManager;

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, false, false);
        if (result.Succeeded) return RedirectToPage("/Index");
        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return Page();
    }
}