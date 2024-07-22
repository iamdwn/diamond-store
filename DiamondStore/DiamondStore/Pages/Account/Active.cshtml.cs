using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Interface;

namespace RazorPage.Pages.Account
{
    public class ActiveModel : PageModel
    {
        private readonly IUserAccountService _userService;
        private readonly ILogger<ActiveModel> _logger;

        public ActiveModel(IUserAccountService userService, ILogger<ActiveModel> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task<IActionResult> OnGet(string email)
        {
            var user = await _userService.GetUser(c => c.Email.Equals(email));

            if (user == null)
            {
                return NotFound();
            }

            user.Status = "Active";

            var result = await _userService.UpdateUser(user);

            if (result)
            {
                return RedirectToPage("/Account/Login");
            }

            return RedirectToPage("/Account/Login");
        }
    }
}
