using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BussinessObject.Models;
using Service.Interface;

namespace DiamondStore.Pages.DeliveryManagepage
{
    public class CreateModel : PageModel
    {
        private readonly IDeliverymanagement _context;

        public CreateModel(IDeliverymanagement context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet()
        {
            var manager = await _context.GetManagerList();
            var order = await _context.GetOrderList();
            var shipper = await _context.GetShipperList();

            ViewData["ManagerId"] = new SelectList(manager, "UserId", "Email");
        ViewData["OrderId"] = new SelectList(order, "OrderId", "Id");
        ViewData["ShiperId"] = new SelectList(shipper, "UserId", "Email");
            return  Page();
        }

        [BindProperty]
        public Delivery Delivery { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            

            _context.AddAsync(Delivery);
            

            return RedirectToPage("./Index");
        }
    }
}
