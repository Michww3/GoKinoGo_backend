using GoKinoGo.Entities;

namespace GoKinoGo.DataAccess.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUserNameAsync(string userName);
    Task<bool> ExistsByEmailAsync(string email);
    Task<bool> ExistsByUserNameAsync(string userName);
    Task<User?> GetWithLikedCommentsAsync(int userId);
}
