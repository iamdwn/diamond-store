using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BussinessObject.Models;
using Service.Interface;

namespace DiamondStore.Pages.Warranties
{
    public class DetailsModel : PageModel
    {
        private readonly IWarrantyService _context;

        public DetailsModel(IWarrantyService context)
        {
            _context = context;
        }

        public Warranty Warranty { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var warranty = await _context.GetByIdAsync(id.ToString());

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
    }
}
