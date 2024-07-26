using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BussinessObject.Models;
using Service.Interface;

namespace DiamondStore.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly IProductService _productService;

        public CreateModel(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> OnGet()
        {
            Categories = await _productService.GetListCategory();
            return Page();
        }

        [BindProperty]
        public Product Products { get; set; } = default!;

        [BindProperty]
        public IEnumerable<Category> Categories { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var result = await _productService.AddAsync(Products);

            if (result == null)
            {
                //Log error
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
