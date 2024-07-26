using BussinessObject.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Interface;

namespace DiamondStore.Pages.DeliveryPage
{
    public class IndexModel : PageModel
    {
        public readonly IDeliveryService _deliveryService;

        public IndexModel(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        public IList<DeliveryResponse> DeliveryList { get; set; } = default!;
        public string product { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var file = await _deliveryService.ExportRevenue();
            return file;
        }
        public async Task<IActionResult> OnGetAsync(int currentPage = 1)
        {
            //Check xem nguoi dang dang nhap co phai la admin khong
            var UserRole = HttpContext.Session.GetInt32("UserRole");
            if (UserRole != 4)
            {
                return Redirect("../Login");
            }

            DeliveryList = await _deliveryService.GetDeliveryResponsesByAdmin();

            foreach (var delivery in DeliveryList)
            {
                product = string.Join(",", delivery.Product);
            }

            CurrentPage = currentPage;
            int pageSize = 5;

            int total = DeliveryList.Count();

            TotalPages = (int)Math.Ceiling((double)total / pageSize);

            DeliveryList = DeliveryList.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            return Page();
        }
    }
}
