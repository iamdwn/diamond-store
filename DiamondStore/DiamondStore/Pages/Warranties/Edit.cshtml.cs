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
using Service.Implement;

namespace DiamondStore.Pages.Warranties
{
    public class EditModel : PageModel
    {
        private readonly IWarrantyService _context;
        private readonly IProductService _productService;


        public EditModel(IWarrantyService context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        [BindProperty]
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
            Warranty = warranty;

            var products = await _productService.GetAllAsync();
            //var users = await _userService.GetAllAsync();
            var customerlist = await _context.GetCustomerList();

            ViewData["ProductId"] = new SelectList(products, "ProductId", "Name");
            ViewData["UserId"] = new SelectList(customerlist, "UserId", "Email");
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

            //_context.Attach(Warranty).State = EntityState.Modified;

            try
            {
                await _context.UpdateAsync(Warranty);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WarrantyExists(Warranty.WarrantyId))
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

        private bool WarrantyExists(Guid id)
        {
            if (_context.GetByIdAsync(id.ToString()) == null)
            {
                return false;
            }
            return true;
        }
    }
}
