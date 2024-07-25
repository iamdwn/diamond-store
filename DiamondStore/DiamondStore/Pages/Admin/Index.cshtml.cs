using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BussinessObject.Models;
using Service.Interface;
using Service.Implement;
using BussinessObject.DTO;
using Service;

namespace DiamondStore.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private IUserAccountService _userAccountService;

        public IndexModel(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        public IList<UserDTO> user { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            //Check xem nguoi dang dang nhap co phai la admin khong
            var UserRole = HttpContext.Session.GetInt32("UserRole");
            if (UserRole != 4)
            {
                return Redirect("../Login");
            }

            user = await _userAccountService.GetAllAsyncByAdmin();
            return Page();
         
        }
    }
}
