using GoKinoGo.Data;
using GoKinoGo.DataAccess.Repositories;
using GoKinoGo.DataAccess.Repositories.Interfaces;
using GoKinoGo.DataAccess.UnitOfWork;
using GoKinoGo.Entities;
using GoKinoGo.Mapping;
using GoKinoGo.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

namespace GoKinoGo;

public static partial class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var jwt = builder.Configuration
                    .GetSection("Jwt")
                    .Get<JwtOptions>()!;

                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwt.Issuer,
                        ValidAudience = jwt.Audience,

                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(jwt.Key))
                    };
            });

        builder.Services.AddAuthorization();

        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        builder.Services.AddScoped<IMovieRepository, MovieRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ICommentRepository, CommentRepository>();
        builder.Services.AddScoped<ILikeRepository, LikeRepository>();
        builder.Services.AddScoped<IRepository<Genre>, Repository<Genre>>();

        builder.Services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<CommentProfile>();
            cfg.AddProfile<GenreProfile>();
            cfg.AddProfile<MovieProfile>();
            cfg.AddProfile<UserProfile>();
        });

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "GoKinoGo",
                Version = "v1",
                Description = "API for movie poster application"
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

        });

        var app = builder.Build();

        app.UseMiddleware<ExceptionMiddleware>();

        app.UseAuthentication();
        app.UseAuthorization();

        await app.RunAsync();
    }
}