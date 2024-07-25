using BussinessObject.DTO;
using BussinessObject.Models;
using Repository.Dtos;
using Repository.Interface;
using Service.Interface;
using Service.Services.Impl;
using System.Linq.Expressions;

namespace Service.Implement
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IBaseCRUD<User> _repo;
        private readonly IEmailQueue _emailQueue;
        private readonly IRoleService _roleService;


        public UserAccountService(IBaseCRUD<User> repo, IEmailQueue emailQueue, IRoleService roleService)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _emailQueue = emailQueue;
            _roleService = roleService;
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

        public async Task<User?> Login(string email, string password)
        {
            var user = await _repo.Find(c => c.Email.Equals(email));

            if (user == null) return null;

            if (!user.Status.Equals("Active")) return null;

            var isMatch = user.Password?.Equals(password) ?? false;

            if (isMatch == false) return null;

            return user;
        }

        public async Task<bool> Register(RegisterDto dto)   
        {
            var existUser = await _repo.Find(c => c.Email.Equals(dto.email));
            //var role = await _itemRepo.Find(r => r.RoleName.Equals("Customer"));
            var role = await _roleService.GetRoleByName("Customer");

            if (existUser != null) return false;

            var newUser = new User
            {
                Username = dto.username,
                Email = dto.email,
                Password = dto.password,
                Status = "Verifying",
                RoleId = role.RoleId
            };

            var result = await _repo.CreateAsync(newUser);

            if (result)
            {
                await _emailQueue.EnqueueEmailAsync(newUser.Email);
            }

            return result;
        }

        public async Task<User?> GetUser(Expression<Func<User, bool>> predicate)
        {
            return await _repo.Find(predicate);
        }

        public async Task<bool> UpdateUser(User user)
        {
            var result = await _repo.Update(user);
            return result;
        }
    }
}
