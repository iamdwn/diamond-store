using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BussinessObject.Models;

namespace DiamondStore.Pages.OrderItems
{
    public class CreateModel : PageModel
    {
        private readonly BussinessObject.Models.DiamondStoreContext _context;

        public CreateModel(BussinessObject.Models.DiamondStoreContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        //ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "Status");
        //ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name");
            return Page();
        }

        [BindProperty]
        public OrderItem OrderItem { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            
            await _context.AddAsync(OrderItem);

            return RedirectToPage("./Index");
        }
    }
}
