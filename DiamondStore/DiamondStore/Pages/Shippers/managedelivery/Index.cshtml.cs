using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BussinessObject.Models;
using Service.Interface;

namespace DiamondStore.Pages.Shippers.managedelivery
{
    public class IndexModel : PageModel
    {
        private readonly IDeliverymanagement _context;
        // private readonly BussinessObject.Models.DiamondStoreContext _context;

        public IndexModel(IDeliverymanagement context)
        {
            _context = context;
        }

        public IList<Delivery> Delivery { get; set; } = default!;

        public async Task OnGetAsync()
        {
            string userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                var de = await _context.GetAllAsync();
                Delivery = de.ToList();
            }
            else
            {
                var de = await _context.GetAllAsyncShipper(Guid.Parse(userId));
            }
        }
    }
}
