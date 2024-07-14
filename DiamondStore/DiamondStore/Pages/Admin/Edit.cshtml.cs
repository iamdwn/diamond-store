using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BussinessObject.Models;
using BussinessObject.DTO;
using Service.Interface;
using System.Data;

namespace DiamondStore.Pages.Admin
{
    public class EditModel : PageModel
    {
        private readonly IUserAccountService _userAccountService;
        private readonly IRoleService _roleService;

        public EditModel(IUserAccountService userAccountService, IRoleService roleService)
        {
            _userAccountService = userAccountService;
            _roleService = roleService;
        }

        [BindProperty]
        public UserDTO user { get; set; } = default!;

        Dictionary<int, string> roles = new Dictionary<int, string>
            {
                { 1, "Customer" },
                { 2, "Shipper" },
                { 3, "Manager" },
                { 4, "Admin" },
                { 5, "Support" }
            };

        public List<string> Status = new List<string> { "Active", "Inactive" };

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //Bắt lỗi validation

             user = await _userAccountService.GetByIdAsyncByAdmin(id.ToString());

            if (user == null)
            {
                return NotFound();
            }


            ViewData["RoleId"] = new SelectList(roles,"Key","Value");
            ViewData["StatusState"] = new SelectList(Status);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            var data = await _userAccountService.GetByIdAsyncByAdmin(user.UserId.ToString());

            if (user.Username != null)
            {
                data.Username = user.Username;
            } 

            if (user.Email != null)
            {
                data.Email = user.Email;
            }

            if (user.Status != null)
            {
                data.Status = user.Status;
            }


            if (roles.TryGetValue(int.Parse(user.RoleName), out string roleName))
            {
                var role = await _roleService.GetRoleByName(roleName);
                data.RoleId = role.RoleId;
            }


            await _userAccountService.UpdateAsyncByAdmin(data);


            return RedirectToPage("./Index");
        }

        private bool UserExists(Guid id)
        {
            if (_userAccountService.GetByIdAsyncByAdmin(id.ToString()) != null)
            {
                return true;
            }
            return false;

        }
    }
}
