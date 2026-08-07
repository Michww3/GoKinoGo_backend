using GoKinoGo.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoKinoGo.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<MovieCollection> MovieCollections { get; set; }
    public DbSet<CollectionItem> CollectionItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.UserName)
            .IsUnique();

        modelBuilder.Entity<Genre>()
            .HasIndex(g => g.Name)
            .IsUnique();

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Owner)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.OwnerId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Movie)
            .WithMany(m => m.Comments)
            .HasForeignKey(c => c.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Like>()
            .HasIndex(l => new
            {
                l.UserId,
                l.CommentId
            })
            .IsUnique();

        modelBuilder.Entity<Like>()
            .HasOne(l => l.User)
            .WithMany(u => u.Likes)
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Like>()
            .HasOne(l => l.Comment)
            .WithMany(c => c.Likes)
            .HasForeignKey(l => l.CommentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Movie>()
            .HasMany(m => m.Genres)
            .WithMany(g => g.Movies);

        modelBuilder.Entity<Movie>()
            .Property(x => x.Price)
            .HasPrecision(10, 2);

        modelBuilder.Entity<CollectionItem>()
            .HasOne(ci => ci.Movie)
            .WithMany(m => m.CollectionItems)
            .HasForeignKey(ci => ci.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CollectionItem>()
            .HasOne(ci => ci.Collection)
            .WithMany(c => c.Items)
            .HasForeignKey(ci => ci.CollectionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CollectionItem>()
            .HasIndex(ci => new
            {
                ci.CollectionId,
                ci.MovieId
            })
            .IsUnique();

        modelBuilder.Entity<CollectionItem>()
            .HasIndex(ci => new
            {
                ci.CollectionId,
                ci.Position
            })
            .IsUnique();

        modelBuilder.Entity<MovieCollection>()
            .HasIndex(x => x.Name)
            .IsUnique();
    }
}