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
    public IGenreRepository Genres { get; } = new GenreRepository(context);
    public ILikeRepository Likes { get; } = new LikeRepository(context);
    public IMovieCollectionRepository MovieCollections { get; } = new MovieCollectionRepository(context);
    public IMovieRatingRepository MovieRatings { get; } = new MovieRatingRepository(context);
    public ICollectionItemRepository CollectionItems { get; } = new CollectionItemRepository(context);
    public IEmailVerificationTokenRepository EmailVerificationTokens { get; } = new EmailVerificationTokenRepository(context);
    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
}
