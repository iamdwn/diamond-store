using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System.Linq.Expressions;

namespace Repository.Implement
{
    public class UserAccountRepo : IBaseCRUD<User>
    {
        public async Task<User> GetByIdAsync(string id)
        {
            try
            {
                using (var _content = new DiamondStoreContext())
                {
                    return await _content.Users.FirstAsync(x => x.UserId == Guid.Parse(id));
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> UpdateAsync(User entity)
        {
            try
            {
                using (var _context = new DiamondStoreContext())
                {
                    var user = await _context.Users.FindAsync(entity.UserId);
                    if (user != null)
                    {
                        user.Password = entity.Password;
                        user.Email = entity.Email;
                        await _context.SaveChangesAsync();
                    }
                    return user;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<User> AddAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            using (var context = new DiamondStoreContext())
            {
                return await context.Users.ToListAsync();
            }
        }
    }
}
