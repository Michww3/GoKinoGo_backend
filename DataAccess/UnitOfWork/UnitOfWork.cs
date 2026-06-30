using GoKinoGo.Data;
using GoKinoGo.DataAccess.Repositories;
using GoKinoGo.DataAccess.Repositories.Interfaces;
using GoKinoGo.Entities;

namespace GoKinoGo.DataAccess.UnitOfWork;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private readonly AppDbContext _context = context;

    public IMovieRepository MovieRepository { get; } = new MovieRepository(context);
    public IUserRepository UserRepository { get; } = new UserRepository(context);
    public ICommentRepository CommentRepository { get; } = new CommentRepository(context);
    public IRepository<Genre> GenreRepository { get; } = new Repository<Genre>(context);
    public IRepository<Like> LikeRepository { get; } = new Repository<Like>(context);
    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
}
