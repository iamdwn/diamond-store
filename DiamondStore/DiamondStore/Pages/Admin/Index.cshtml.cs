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

namespace DiamondStore.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private IUserAccountService _userAccountService;

        public IndexModel(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService ?? throw new ArgumentNullException(nameof(userAccountService));
        }

        //private readonly BussinessObject.Models.DiamondStoreContext _context;

        //public IndexModel(BussinessObject.Models.DiamondStoreContext context)
        //{
        //    _context = context;
        //}

        public IEnumerable<User> user { get;set; } = default!;

        public async Task OnGetAsync()
        {
            //Check xem nguoi dang dang nhap co phai la admin khong
            //if (HttpContext.Session.GetString("Role") != "Admin")
            //{
            //    Response.Redirect("/Login");
            //}

            user = await _userAccountService.GetAllAsync();


            //User = await _context.Users
            //    .Include(u => u.Role).ToListAsync();

        }
    }
}
