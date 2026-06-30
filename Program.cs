using GoKinoGo.Data;
using GoKinoGo.DataAccess.Repositories;
using GoKinoGo.DataAccess.Repositories.Interfaces;
using GoKinoGo.DataAccess.UnitOfWork;
using GoKinoGo.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GoKinoGo;

public static partial class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        builder.Services.AddScoped<IMovieRepository, MovieRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ICommentRepository, CommentRepository>();

        builder.Services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<MovieProfile>();
            cfg.AddProfile<GenreProfile>();
            cfg.AddProfile<CommentProfile>();
            cfg.AddProfile<UserProfile>();
        });

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        await app.RunAsync();
    }
}