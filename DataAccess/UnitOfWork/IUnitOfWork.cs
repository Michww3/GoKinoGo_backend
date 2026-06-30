using GoKinoGo.DataAccess.Repositories.Interfaces;
using GoKinoGo.Entities;

namespace GoKinoGo.DataAccess.UnitOfWork;

public interface IUnitOfWork
{
    IMovieRepository MovieRepository { get; }
    IUserRepository UserRepository { get; }
    ICommentRepository CommentRepository { get; }
    IRepository<Genre> GenreRepository { get; }
    IRepository<Like> LikeRepository {  get; }
    Task<int> SaveChangesAsync();
}
