using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.DTO
{
    public class UserDTO
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public Guid? RoleId { get; set; }
        public string RoleName { get; set; }
        public string Status { get; set; } = null!;

    }
}
