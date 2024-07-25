using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BussinessObject.Models;
using Service.Interface;

namespace DiamondStore.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IOrderItemService _orderItemService;

        public IndexModel(IProductService productService, IOrderItemService orderItemService)
        {
            _productService = productService;
            _orderItemService = orderItemService;
        }

        public IList<Product> Products { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var products = await _productService.GetAllAsync();
            Products = products.ToList();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(string id)
        {
            var newOrderItem = new OrderItem
            {
                ProductId = Guid.Parse(id),
            };
            await _orderItemService.AddAsync(newOrderItem);
            return RedirectToPage();
        }
    }
}
