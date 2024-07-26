using BussinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Interface;

namespace DiamondStore.Pages
{
    public class CartModel : PageModel
    {
        private readonly IOrderItemService _context;

        public CartModel(IOrderItemService context)
        {
            _context = context;
        }

        public IList<OrderItem> OrderItem { get; set; } = default!;

        [FromQuery(Name = "orderId")]
        public Guid OrderId { get; set; }

        public async Task OnGetAsync()
        {
            string userId = HttpContext.Session.GetString("UserId");
            OrderItem = await _context.GetItemOfCartByUserId(userId);
            if (OrderItem == null) { OrderItem = new List<OrderItem>(); }
        }
    }
}
