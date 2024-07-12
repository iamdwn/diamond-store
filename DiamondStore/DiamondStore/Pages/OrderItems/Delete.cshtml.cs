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
    public class DeleteModel : PageModel
    {
        private readonly IOrderItemService _context;

        public DeleteModel(IOrderItemService context)
        {
            _context = context;
        }

        [BindProperty]
        public OrderItem OrderItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderitem = await _context.GetByIdAsync(id.ToString());

            if (orderitem == null)
            {
                return NotFound();
            }
            else
            {
                OrderItem = orderitem;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderitem = await _context.GetByIdAsync(id.ToString());

            if (orderitem != null)
            {
                await _context.DeleteAsync(id.ToString());
            }

            return RedirectToPage("./Index");
        }
    }
}
