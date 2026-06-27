using GoKinoGo.Data;
using GoKinoGo.DataAccess.Interfaces;
using GoKinoGo.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoKinoGo.DataAccess;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) { }
    public async Task<bool> ExistsByEmailAsync(string email) => await _dbSet.AnyAsync(u => u.Email == email);

    public async Task<bool> ExistsByUserNameAsync(string userName) => await _dbSet.AnyAsync(u => u.UserName == userName);

    public async Task<User?> GetByEmailAsync(string email) => await _dbSet.SingleOrDefaultAsync(u => u.Email == email);

    public async Task<User?> GetByUserNameAsync(string userName) => await _dbSet.SingleOrDefaultAsync(u => u.UserName == userName);

    public async Task<User?> GetWithLikedCommentsAsync(int userId)
    {
        return await _dbSet
            .Include(u => u.LikedComments)
            .SingleOrDefaultAsync(u => u.Id == userId);
    }
}
