using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PMS.Models.Models;
using PMS.Utilities;

namespace PMS.Web.Pages.Account;

public class LoginModel : PageModel
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public LoginModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public string? ReturnUrl { get; set; }

    public class InputModel
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
    }

    public void OnGet(string? returnUrl = null)
    {
        ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");

        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user != null)
            {
                if (user.Status == UserStatus.Disabled)
                {
                    ModelState.AddModelError(string.Empty, "Your account has been disabled.");
                    return Page();
                }

                var result = await _signInManager.PasswordSignInAsync(user.UserName!, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    // Redirect based on role
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles.Contains(Roles.Admin))
                    {
                        return RedirectToPage("/Admin/Index");
                    }
                    else if (roles.Contains(Roles.Agent))
                    {
                        return RedirectToPage("/Agent/Index");
                    }
                    else if (roles.Contains(Roles.Tenant))
                    {
                        return RedirectToPage("/Tenant/Index");
                    }

                    return LocalRedirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }
        }

        return Page();
    }
}
