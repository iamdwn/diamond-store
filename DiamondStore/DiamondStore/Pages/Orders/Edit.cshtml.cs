using BussinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Service.Interface;

namespace DiamondStore.Pages.Orders
{
    public class EditModel : PageModel
    {
        private readonly BussinessObject.Models.DiamondStoreContext _context;
        private readonly IOrderService _orderService;

        public EditModel(BussinessObject.Models.DiamondStoreContext context, IOrderService orderService)
        {
            _context = context;
            _orderService = orderService;
        }

        [BindProperty]
        public Order Order { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var order = await _context.Orders.FirstOrDefaultAsync(m => m.OrderId == id);
            var order = await _orderService.GetByIdAsync(id.ToString());

            if (order == null)
            {
                return NotFound();
            }
            Order = order;
            //ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            //ViewData["VoucherId"] = new SelectList(_context.Vouchers, "VoucherId", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(Order.OrderId))
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

        private bool OrderExists(Guid id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
