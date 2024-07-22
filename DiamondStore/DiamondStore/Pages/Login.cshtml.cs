using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using Service.Dtos;
using Service.Interface;
using System.ComponentModel.DataAnnotations;

namespace DiamondStore.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUserAccountService _userService;
        public LoginModel(IUserAccountService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.Login(Input.Email, Input.Password);
                if (result != null)
                {
                    if (!result.Status.Equals("Active"))
                    {
                        TempData["toast-error"] = "Verify gmail not yet!";
                        return Page();
                    }

                    TempData["toast-success"] = "Login success!";

                    var user = new UserDto
                    {
                        isAuthenticated = true,
                        userId = result.UserId,
                        username = result.Username,
                    };

                    //HttpContext.Session.SetInt32("UserId", result.CxustomerId);
                    HttpContext.Session.SetObjectAsJson("User", user);
                    HttpContext.Session.SetString("IsAuthenticated", "true");
                    return RedirectToPage("/Index");
                }
                else
                {
                    TempData["toast-error"] = "Login failed!";
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
            return Page();
        }
    }
}
