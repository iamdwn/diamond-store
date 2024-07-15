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

        public async Task OnGetAsync()
        {
            DeliveryList = await _deliveryService.GetDeliveryResponsesByAdmin();

            foreach (var delivery in DeliveryList)
            {
                product = string.Join(",", delivery.Product);
            }
        }
    }
}
