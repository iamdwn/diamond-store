﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BussinessObject.Models;

namespace DiamondStore.Pages.Warranties
{
    public class DeleteModel : PageModel
    {
        private readonly BussinessObject.Models.DiamondStoreContext _context;

        public DeleteModel(BussinessObject.Models.DiamondStoreContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Warranty Warranty { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warranty = await _context.Warranties.FirstOrDefaultAsync(m => m.WarrantyId == id);

            if (warranty == null)
            {
                return NotFound();
            }
            else
            {
                Warranty = warranty;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warranty = await _context.Warranties.FindAsync(id);
            if (warranty != null)
            {
                Warranty = warranty;
                _context.Warranties.Remove(Warranty);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
