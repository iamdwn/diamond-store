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
    public class IndexModel : PageModel
    {
        private readonly IDeliverymanagement _context;
       // private readonly BussinessObject.Models.DiamondStoreContext _context;

        public IndexModel(IDeliverymanagement  context)
        {
            _context = context;
        }

        public IList<Delivery> Delivery { get;set; } = default!;

        public async Task OnGetAsync()
        {
           var de = await _context.GetAllAsync();
            Delivery = de.ToList();
        }
    }
}
