using BussinessObject.Models;
using Repository.Interface;
using Service.Interface;

namespace Service.Implement
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IBaseCRUD<User> _repo;

        public UserAccountService(IBaseCRUD<User> repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public async Task<User> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("ID cannot be null or empty.", nameof(id));
            }

            var user = await _repo.GetByIdAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            return user;
        }

        public async Task UpdateAsync(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "User entity cannot be null.");
            }

            var existingUser = await _repo.GetByIdAsync(entity.UserId.ToString());
            if (existingUser == null)
            {
                throw new KeyNotFoundException($"User with ID {entity.UserId} not found.");
            }

            await _repo.UpdateAsync(entity);
        }
    }
}
