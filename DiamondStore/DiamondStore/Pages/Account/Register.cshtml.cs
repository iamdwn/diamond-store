using BussinessObject.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Dtos;
using Service.Dtos;
using Service.Interface;

namespace RazorPage.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IUserAccountService _userService;
        public RegisterModel(IUserAccountService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public RegisterDto Input { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.Register(Input);

                if (result)
                {
                    TempData["toast-success"] = "Register success!";
                    return RedirectToPage("/Account/Login");
                }
                else
                {
                    TempData["toast-error"] = "Register failed!";
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
