using BussinessObject.Models;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IUserAccountService
    {
        Task<User> GetByIdAsync(string id);

        Task UpdateAsync(User entity);

        Task<IEnumerable<User>> GetAllAsync();
    }
}