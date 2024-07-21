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

namespace DiamondStore.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly IProductService _productService;

        public EditModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            Product = product;
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            //_context.Attach(Product).State = EntityState.Modified;

            try
            {
                await _productService.DeleteAsync(Product.ProductId.ToString());
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await ProductExists(Product.ProductId.ToString()))
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

        private async Task<bool> ProductExists(string id)
        {
            var result = await _productService.GetByIdAsync(id);

            return result == null;
        }
    }
}
