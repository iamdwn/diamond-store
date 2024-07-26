using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BussinessObject.Models;
using Service.Interface;
using Service;
using Service.Dtos;

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
            var user = HttpContext.Session.GetObjectFromJson<UserDto>("User");
            await _orderItemService.AddProductToOrder(user.userId.ToString(), id);
            return RedirectToPage();
        }
    }
}
