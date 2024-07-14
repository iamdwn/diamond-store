using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class RoleRepo : IBaseCRUD<Role>
    {
        public Task<Role> AddAsync(Role entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Role>> FindAsync(Expression<Func<Role, bool>> predicate)
        {
            try
            {
                using (var _content = new DiamondStoreContext())
                {
                    return await _content.Roles.Where(predicate).ToListAsync();
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<IEnumerable<Role>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Role> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Role> UpdateAsync(Role entity)
        {
            throw new NotImplementedException();
        }

    }
}
