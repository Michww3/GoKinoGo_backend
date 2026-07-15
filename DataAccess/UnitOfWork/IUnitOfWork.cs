using GoKinoGo.DataAccess.Repositories.Interfaces;
using GoKinoGo.Entities;

namespace GoKinoGo.DataAccess.UnitOfWork;

public interface IUnitOfWork
{
    IMovieRepository Movies { get; }
    IUserRepository Users { get; }
    ICommentRepository Comments { get; }
    IGenreRepository Genres { get; }
    ILikeRepository Likes { get; }
    Task<int> SaveChangesAsync();
}
