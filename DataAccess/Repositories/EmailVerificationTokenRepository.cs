using GoKinoGo.Data;
using GoKinoGo.DataAccess.Repositories.Interfaces;
using GoKinoGo.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoKinoGo.DataAccess.Repositories;

public class EmailVerificationTokenRepository(AppDbContext context) : Repository<EmailVerificationToken>(context), IEmailVerificationTokenRepository
{
    public async Task<EmailVerificationToken?> GetByTokenHashAsync(string tokenHash)
    {
        return await _dbSet.Include(e => e.User).FirstOrDefaultAsync(e => e.TokenHash == tokenHash);
    }

    public async Task<IEnumerable<EmailVerificationToken>> GetByUserIdAsync(int userId)
    {
        return await _dbSet
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }
}
