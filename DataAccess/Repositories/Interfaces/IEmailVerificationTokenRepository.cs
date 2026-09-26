using GoKinoGo.Entities;

namespace GoKinoGo.DataAccess.Repositories.Interfaces;

public interface IEmailVerificationTokenRepository : IRepository<EmailVerificationToken>
{
    Task<EmailVerificationToken?> GetByTokenHashAsync(string tokenHash);
    Task<IEnumerable<EmailVerificationToken>> GetByUserIdAsync(int userId);
}
