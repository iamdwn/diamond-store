using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BussinessObject.Models;
using Service.Interface;

namespace DiamondStore.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private readonly IOrderService _context;

        public CreateModel(IOrderService orderService)
        {
            _context = orderService;
        }

        public IActionResult OnGet()
        {
        //ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
        //ViewData["VoucherId"] = new SelectList(_context.Vouchers, "VoucherId", "Name");
            return Page();
        }

        [BindProperty]
        public Order Order { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            await _context.AddAsync(Order);

            return RedirectToPage("./Index");
        }
    }
}
