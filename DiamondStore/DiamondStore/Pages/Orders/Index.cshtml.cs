using BussinessObject.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Interface;

namespace DiamondStore.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly IOrderService _orderService;

        public IndexModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IList<Order> Order { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Order = await _orderService.GetAllAsync();
        }
    }
}
