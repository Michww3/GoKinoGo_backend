using GoKinoGo.Data;
using GoKinoGo.DataAccess.Repositories;
using GoKinoGo.DataAccess.Repositories.Interfaces;
using GoKinoGo.Entities;

namespace GoKinoGo.DataAccess.UnitOfWork;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private readonly AppDbContext _context = context;

    public IMovieRepository Movies { get; } = new MovieRepository(context);
    public IUserRepository Users { get; } = new UserRepository(context);
    public ICommentRepository Comments { get; } = new CommentRepository(context);
    public IRepository<Genre> Genres { get; } = new Repository<Genre>(context);
    public IRepository<Like> Likes { get; } = new Repository<Like>(context);
    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
}
