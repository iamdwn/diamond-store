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
using BussinessObject.DTO;
using Service;
using System.Numerics;

namespace DiamondStore.Pages.Admin
{
    public class HistoryModel : PageModel
    {
        private IDeliveryService _deliveryService;

        public HistoryModel(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        [BindProperty]
        public IList<DeliveryResponse> revenue { get;set; } = default!;

        public decimal TotalPrice { get; set; }
        [BindProperty]
        public DateOnly SelectedDate { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            //Check xem nguoi dang dang nhap co phai la admin khong
            var userDto = HttpContext.Session.GetObjectFromJson<UserDTO>("User");
            if (userDto != null)
            {
                var role = userDto.RoleId.ToString();
                if (!role.Equals("c423e182-4af3-4451-9d60-41e77ff23b0f"));
                {
                    return Redirect("/Login");
                }

            }

            var delivery = await _deliveryService .FindAsync(x => x.Order.OrderDate <= SelectedDate);

            foreach(var item in delivery)
            {
            }




            return Page();

        }
    }
}
