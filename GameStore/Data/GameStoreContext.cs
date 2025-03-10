using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Game> Genres => Set<Game>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
            new { Id = 1, Name = "Fighting Tiger" },
            new { Id = 2, Name = "Adventure Quest" },
            new { Id = 3, Name = "Mystery Mansion" },
            new { Id = 4, Name = "Space Odyssey" },
            new { Id = 5, Name = "Puzzle Paradise" }
        );
    }
}
