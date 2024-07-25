using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BussinessObject.Models;
using Service.Interface;

namespace DiamondStore.Pages.DeliveryManagepage
{
    public class DetailsModel : PageModel
    {
       private readonly IDeliveryService _context;

        public DetailsModel(IDeliveryService context)
        {
            _context = context;
        }

        public Delivery Delivery { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
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
            else
            {
                Delivery = delivery;
            }
            return Page();
        }
    }
}
