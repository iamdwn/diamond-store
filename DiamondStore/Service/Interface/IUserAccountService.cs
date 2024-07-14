using BussinessObject.DTO;
using BussinessObject.Models;
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

    }
}