using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BussinessObject.Models;
using Service.Interface;

namespace DiamondStore.Pages.OrderItems
{
    public class EditModel : PageModel
    {
        private readonly IOrderItemService _context;

        public EditModel(IOrderItemService context)
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
            OrderItem = orderitem;
           //ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "Status");
           //ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _context.UpdateAsync(OrderItem);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderItemExists(OrderItem.OrderItemId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool OrderItemExists(Guid id)
        {
            if (_context.GetByIdAsync(id.ToString()) == null)
            {
                return false;
            }
            return true;
        }
    }
}
