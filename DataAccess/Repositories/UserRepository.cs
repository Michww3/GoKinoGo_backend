using GoKinoGo.Data;
using GoKinoGo.DataAccess.Repositories.Interfaces;
using GoKinoGo.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoKinoGo.DataAccess.Repositories;

public class UserRepository(AppDbContext context) : Repository<User>(context), IUserRepository
{
    public async Task<bool> ExistsByEmailAsync(string email) => await _dbSet.AnyAsync(u => u.Email == email);

    public async Task<bool> ExistsByUserNameAsync(string userName) => await _dbSet.AnyAsync(u => u.UserName == userName);

    public async Task<User?> GetByEmailAsync(string email) => await _dbSet.SingleOrDefaultAsync(u => u.Email == email);

    public async Task<User?> GetByUserNameAsync(string userName) => await _dbSet.SingleOrDefaultAsync(u => u.UserName == userName);

    public async Task<User?> GetWithLikesAsync(int userId)
    {
        return await _dbSet
            .Include(u => u.Likes)
            .SingleOrDefaultAsync(u => u.Id == userId);
    }
}
