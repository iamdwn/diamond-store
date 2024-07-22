using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
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
                    return await _content.Users.Include(r => r.Role).FirstAsync(x => x.UserId == Guid.Parse(id));
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
                        _context.Entry(user).CurrentValues.SetValues(entity);
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
                return await context.Users.Include(r => r.Role).ToListAsync();
            }
        }

        public async Task<User> Find(Expression<Func<User, bool>> predicate)
        {
            using (var context = new DiamondStoreContext())
            {
                return await context.Users.Include(r => r.Role).FirstOrDefaultAsync(predicate);
            }
        }

        public async Task<bool> CreateAsync(User entity)
        {
            using (var context = new DiamondStoreContext())
            {
                context.Users.Add(entity);
                return await context.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> Update(User entity)
        {
            using (var context = new DiamondStoreContext())
            {
                var existingUser = await context.Users.FirstOrDefaultAsync(c => c.Email.Equals(entity.Email));
                if (existingUser != null)
                {
                    existingUser.Username = entity.Username ?? existingUser.Username;
                    existingUser.Email = entity.Email ?? existingUser.Email;
                    existingUser.Status = entity.Status ?? existingUser.Status;
                    existingUser.Password = entity.Password ?? existingUser.Password;

                    //context.Users.Update(existingUser);
                    //context.Entry(entity).State = EntityState.Modified;
                    //await context.SaveChangesAsync();
                }

                return await context.SaveChangesAsync() > 0;
            }
        }
    }
}
