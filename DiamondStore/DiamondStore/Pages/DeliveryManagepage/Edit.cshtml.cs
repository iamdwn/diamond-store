using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BussinessObject.Models;
using Service.Interface;
using Service.Implement;

namespace DiamondStore.Pages.DeliveryManagepage
{
    public class EditModel : PageModel
    {
        private readonly IDeliverymanagement _context;
        private readonly IUserAccountService _userAccountService;
        // private readonly BussinessObject.Models.DiamondStoreContext _context;

        public EditModel(IDeliverymanagement context, IUserAccountService userAccountService)
        {
            _context = context;
            _userAccountService = userAccountService;
        }

        [BindProperty]
        public Delivery Delivery { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = await _context.GetByIdAsync(id.ToString());
            if (delivery == null)
            {
                return NotFound();
            }
            Delivery = delivery;
            var manager = await _context.GetManagerList();
            var order  =await _context.GetOrderList();
            var shipper= await _context.GetShipperList();
            ViewData["ManagerId"] = new SelectList(manager, "UserId", "Email");
           ViewData["OrderId"] = new SelectList(order, "OrderId", "Id");
            ViewData["ShiperId"] = new SelectList(shipper, "UserId", "Email");

            Delivery = delivery;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
           

            //   _context.Attach(Delivery).State = EntityState.Modified;

            try
            {
                await _context.UpdateAsync(Delivery);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await DeliveryExists(Delivery.DeliveryId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> DeliveryExists(Guid id)
        {
            var exist = await _context.FindAsync(e => e.DeliveryId == id);
            return exist == null;
        }
    }
}
