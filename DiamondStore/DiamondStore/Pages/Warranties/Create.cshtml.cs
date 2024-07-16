using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BussinessObject.Models;
using Service.Interface;

namespace DiamondStore.Pages.Warranties
{
    public class CreateModel : PageModel
    {
        private readonly IWarrantyService _context;

        public CreateModel(IWarrantyService context)
        {
            _context = context;
        }

        public async Task<IActionResult>  OnGet()
        {
            //ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name");
            //ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            return Page();
        }

        [BindProperty]
        public Warranty Warranty { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            _context.AddAsync(Warranty);
            //await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
