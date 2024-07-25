using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BussinessObject.Models;
using Service.Interface;

namespace DiamondStore.Pages.OrderItems
{
    public class IndexModel : PageModel
    {
        private readonly IOrderItemService _context;

        public IndexModel(IOrderItemService context)
        {
            _context = context;
        }

        public IList<OrderItem> OrderItem { get;set; } = default!;

        [FromQuery(Name = "orderId")]
        public Guid OrderId { get; set; }

        public async Task OnGetAsync(Guid? orderId)
        {
            if (orderId.HasValue)
            {
                // Lấy danh sách OrderItem theo OrderId
                OrderItem = (await _context.FindAsync(oi => oi.OrderId == orderId)).ToList();
            }
            else
            {
                // Trường hợp không có OrderId, có thể hiển thị thông báo lỗi hoặc chuyển hướng.
                OrderItem = new List<OrderItem>();
            }
        }
    }
}
