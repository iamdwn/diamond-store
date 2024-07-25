using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BussinessObject.Models;
using Service.Interface;
using Microsoft.Identity.Client;

namespace DiamondStore.Pages.DeliveryManagepage
{
    public class DetailsModel : PageModel
    {
        private readonly IDeliverymanagement _context;
        private readonly IOrderService _order;

        private readonly IOrderItemService _item;


        public DetailsModel(IDeliverymanagement context, IOrderService service,  IOrderItemService item)
        {
            
            _context = context;
            _order = service;
            _item = item;
        }
        public IList<OrderItem> OrderItem { get; set; }
        public Delivery Delivery { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = await _context.GetByIdAsync(id.ToString());
            //var Order = await _order.GetByIdAsync(delivery.OrderId.ToString());
            //var manager = await _userAccountService.GetByIdAsync(delivery.ManagerId.ToString());
            //delivery.Manager = manager;
            //var shipper = await _userAccountService.GetByIdAsync(delivery.ShiperId.ToString());
            //delivery.Shiper = shipper;
            //delivery.Order = Order;
            if (delivery.OrderId.HasValue)
            {
                // Lấy danh sách OrderItem theo OrderId
                OrderItem = (await _item.FindAsync(oi => oi.OrderId == delivery.OrderId)).ToList();
            }

            if (delivery == null)
            {
                return NotFound();
            }
            else
            {

                Delivery = delivery;
            }
            return Page();
        }
    }
}
