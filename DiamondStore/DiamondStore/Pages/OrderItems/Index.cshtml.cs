using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BussinessObject.Models;
using Service.Interface;

namespace DiamondStore.Pages.OrderItems
{
    public class IndexModel : PageModel
    {
        private readonly IOrderItemService _context;

        public IndexModel(IOrderItemService context)
        {
            _context = context;
        }

        public IList<OrderItem> OrderItem { get;set; } = default!;

        public async Task OnGetAsync()
        {
            OrderItem = await _context.GetAllAsync();
        }
    }
}
