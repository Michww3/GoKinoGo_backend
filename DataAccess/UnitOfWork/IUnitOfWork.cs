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
    IMovieCollectionRepository MovieCollections { get; }
    IMovieRatingRepository MovieRatings { get; }
    ICollectionItemRepository CollectionItems { get; }
    IEmailVerificationTokenRepository EmailVerificationTokens { get; }
    Task<int> SaveChangesAsync();
}
