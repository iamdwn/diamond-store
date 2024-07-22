using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using Service.Dtos;
using Service.Interface;
using System.ComponentModel.DataAnnotations;

namespace RazorPage.Pages.Account
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
            if (HttpContext.Session.GetString("UserRole") != null)
            {
                var role = int.Parse(HttpContext.Session.GetString("UserRole"));
                if (role == 3 || role == 4)
                {
                    Response.Redirect("Admin/Index");
                }
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await _userService.Login(Input.Email, Input.Password);
            if (result != null)
            {
                if (!result.Status.Equals("Active"))
                {
                    //TempData["toast-error"] = "Verify gmail not yet!";
                    ModelState.AddModelError("Error", "Verify gmail not yet!");
                    return Page();
                }

                TempData["toast-success"] = "Login success!";

                var user = new UserDto
                {
                    isAuthenticated = true,
                    userId = result.UserId,
                    username = result.Username,
                };

                HttpContext.Session.SetString("UserRole", result.Role.RoleName);
                HttpContext.Session.SetObjectAsJson("User", user);
                HttpContext.Session.SetString("IsAuthenticated", "true");

                switch (result.Role.Id)
                {
                    //Role Customer
                    case 1:
                        return RedirectToPage("/Index");

                    //Role Shipper
                    case 2:
                        return RedirectToPage("/Shippers/Index");

                    //Role Manager
                    case 3:
                        return RedirectToPage("/Manager/Index");

                    //Role Admin
                    case 4:
                        return RedirectToPage("/Admin/Index");

                    //Role Support
                    case 5:
                        return RedirectToPage("/Support/Index");

                    default:
                        return Page();
                }

                //ModelState.AddModelError("Error", "You do not have permission to do this function!");
                //return Page();
            }
            else
            {
                ModelState.AddModelError("Error", "Invalid email or password!");
                return Page();
            }
        }
    }
}
