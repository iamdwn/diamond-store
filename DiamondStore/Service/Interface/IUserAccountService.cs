using BussinessObject.DTO;
using BussinessObject.Models;
using Repository.Dtos;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IUserAccountService
    {
        Task<User> GetByIdAsync(string id);
        Task<UserDTO> GetByIdAsyncByAdmin(string id);
        Task UpdateAsync(User entity);
        Task UpdateAsyncByAdmin(UserDTO entity);
        Task<List<User>> GetAllAsync();
        Task<List<UserDTO>> GetAllAsyncByAdmin();
        Task<User?> Login(string email, string password);
        Task<bool> Register(RegisterDto dto);
        Task<User?> GetUser(Expression<Func<User, bool>> predicate);
        Task<bool> UpdateUser(User user);
    }
}