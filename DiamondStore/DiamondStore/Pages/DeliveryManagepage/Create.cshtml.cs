using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BussinessObject.Models;

namespace DiamondStore.Pages.DeliveryManagepage
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
        ViewData["ManagerId"] = new SelectList(_context.Users, "UserId", "Email");
        ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "Status");
        ViewData["ShiperId"] = new SelectList(_context.Users, "UserId", "Email");
            return Page();
        }

        [BindProperty]
        public Delivery Delivery { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Deliveries.Add(Delivery);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
