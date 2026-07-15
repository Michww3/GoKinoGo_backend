using GoKinoGo.Data;
using GoKinoGo.DataAccess.Repositories.Interfaces;
using GoKinoGo.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoKinoGo.DataAccess.Repositories;

public class GenreRepository(AppDbContext context) : Repository<Genre>(context), IGenreRepository
{
    public async Task<bool> ExistByNameAsync(string name)
    {
        return await _dbSet.AnyAsync(g => g.Name == name);
    }
}
