using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BussinessObject.Models;
using Service.Interface;
using Service.Implement;

namespace DiamondStore.Pages.Warranties
{
    public class IndexModel : PageModel
    {
        private readonly IWarrantyService _context;


        public IndexModel(IWarrantyService context)
        {
            _context = context;
        }

        public IList<Warranty> Warranty { get; set; } = default!;

        public async Task OnGetAsync()
        {
            //Warranty = await _context.Warranties
            //    .Include(w => w.Product)
            //    .Include(w => w.User).ToListAsync();

            Warranty = (await _context.GetAllAsync()).ToList();
        }
    }
}
