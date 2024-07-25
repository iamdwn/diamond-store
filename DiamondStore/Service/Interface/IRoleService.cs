using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IRoleService
    {
        Task<Role> GetRoleByName(string name); 
        Task<Role> GetRoleById(Guid id); 
    }
}
