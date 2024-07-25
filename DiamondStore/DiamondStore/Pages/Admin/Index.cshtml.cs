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
using Microsoft.IdentityModel.Tokens;

namespace DiamondStore.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private IUserAccountService _userAccountService;

        public IndexModel(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public string Keyword { get; set; }

        public IList<UserDTO> user { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync(int currentPage = 1, string searchTerm = "")
        {
            //Check xem nguoi dang dang nhap co phai la admin khong
            var UserRole = HttpContext.Session.GetInt32("UserRole");
            if (UserRole != 4)
            {
                return Redirect("../Login");
            }

            user = await _userAccountService.GetAllAsyncByAdmin();
            CurrentPage = currentPage;
            int pageSize = 4;
            

            //Search
            if (!searchTerm.IsNullOrEmpty())
            {
                Keyword = searchTerm;
                //Search by FullName or Email
                user = user.Where(x => x.Username.ToLower().Trim().Contains(searchTerm.ToLower().Trim())
                || x.Email.ToLower().Trim().Contains(searchTerm.ToLower().Trim())).ToList();

            }

            int total = user.Count();
            TotalPages = (int)Math.Ceiling((double)total /pageSize);

            user = user.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            return Page();
         
        }
    }
}
