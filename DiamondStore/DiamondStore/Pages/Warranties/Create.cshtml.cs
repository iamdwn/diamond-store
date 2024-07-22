using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BussinessObject.Models;
using Service.Interface;
using Repository.Interface;
using Repository.Implement;
using Service.Implement;

namespace DiamondStore.Pages.Warranties
{
    public class CreateModel : PageModel
    {
        private readonly IWarrantyService _context;
        private readonly IProductService _productService;

        public CreateModel(IWarrantyService context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        public async Task<IActionResult>  OnGet()
        {
            var customerlist =  await _context.GetCustomerList();
            var products = await _productService.GetAllAsync();
            ViewData["ProductId"] = new SelectList(products, "ProductId", "Name");
            ViewData["UserId"] = new SelectList(customerlist, "UserId", "Email");
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
