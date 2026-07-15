using GoKinoGo.Entities;

namespace GoKinoGo.DataAccess.Repositories.Interfaces;

public interface IGenreRepository : IRepository<Genre>
{
    Task<bool> ExistByNameAsync(string name);
}
