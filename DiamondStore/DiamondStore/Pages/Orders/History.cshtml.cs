using BussinessObject.DTO;
using BussinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Dtos;
using Service.Interface;

namespace DiamondStore.Pages.Orders
{
    public class HistoryModel : PageModel
    {
        private readonly IOrderService _orderService;

        public HistoryModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IList<Order> Order { get; set; } = default!;

        public async Task OnGetAsync()
        {
            string userId = HttpContext.Session.GetString("UserId");
            Order = await _orderService.OrderHistory(userId);
        }
    }
}
