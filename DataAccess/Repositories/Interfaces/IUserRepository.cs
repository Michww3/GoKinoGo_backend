using GoKinoGo.Entities;

namespace GoKinoGo.DataAccess.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUserNameAsync(string userName);
    Task<bool> ExistsByEmailAsync(string email);
    Task<bool> ExistsByUserNameAsync(string userName);
    Task<User?> GetWithLikesAsync(int userId);
}
