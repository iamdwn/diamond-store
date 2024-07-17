using BussinessObject.DTO;
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

        public async Task<List<User>> GetAllAsync()
        {
            var data = await _repo.GetAllAsync();
            return data.ToList();
        }

        public async Task<List<UserDTO>> GetAllAsyncByAdmin()
        {
            var data = await _repo.GetAllAsync();
            var result = new List<UserDTO>();

            foreach (var item in data)
            {
                result.Add(toUserDTOs(item));
            }

            return result;
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

        public async Task<UserDTO> GetByIdAsyncByAdmin(string id)
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

            return toUserDTOs(user);
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
        public async Task UpdateAsyncByAdmin(UserDTO entity)
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

            existingUser.Username = entity.Username;
            existingUser.Email = entity.Email;
            existingUser.RoleId = entity.RoleId;
            existingUser.Status = entity.Status;


            await _repo.UpdateAsync(existingUser);
        }

        public User toUserModel(UserDTO userDTO)
        {
            User user = new User
            {
                UserId = userDTO.UserId,
                Username = userDTO.Username,
                Email = userDTO.Email,
                RoleId = userDTO.RoleId,
                Status = userDTO.Status
            };
            return user;
        }

        public UserDTO toUserDTOs(User user)
        {
            UserDTO userDTO = new UserDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                RoleId = user.RoleId,
                RoleName = user.Role.RoleName,
                Status = user.Status
            };
            return userDTO;
        }

        
    }
}
