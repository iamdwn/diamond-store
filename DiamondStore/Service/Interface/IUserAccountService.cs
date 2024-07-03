using BussinessObject.Models;

namespace Service.Interface
{
    public interface IUserAccountService
    {
        Task<User> GetByIdAsync(string id);

        Task UpdateAsync(User entity);
    }
}