using Microsoft.EntityFrameworkCore;
using RiverPuzzle1.Models;

namespace RiverPuzzle1.Data
{
    public class GameContext : DbContext
    {
        public GameContext(DbContextOptions<GameContext> options): base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<GameState> GameState { get; set; }
        public DbSet<Character> Characters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().ToTable("Game");
            modelBuilder.Entity<GameState>().ToTable("GameState");
            modelBuilder.Entity<Character>().ToTable("Character");
        }
    }
}
