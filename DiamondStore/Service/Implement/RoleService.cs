using BussinessObject.Models;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class RoleService : IRoleService
    {
        private readonly IBaseCRUD<Role> _repo;

        public RoleService(IBaseCRUD<Role> repo)
        {
            _repo = repo;
        }

        public async Task<Role> GetRoleByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Role name is null or empty");
            }

            var roles =  await _repo.FindAsync(x => x.RoleName.Equals(name));

            if (roles == null)
            {
                throw new KeyNotFoundException("Role not found");
            }

            var result = roles.FirstOrDefault();

            return result;
        }
    }
}
