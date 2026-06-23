using GoKinoGo.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoKinoGo.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Owner)
            .WithMany(o => o.Comments)
            .HasForeignKey(c => c.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Movie)
            .WithMany(m => m.Comments)
            .HasForeignKey(c => c.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Comment>()
            .HasMany(c => c.LikedByUsers)
            .WithMany(u => u.LikedComments);

        modelBuilder.Entity<Movie>()
                    .HasMany(m => m.Genres)
                    .WithMany(g => g.Movies);
    }

}

