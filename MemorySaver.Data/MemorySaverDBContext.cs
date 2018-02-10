using MemorySaver.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MemorySaver.Data
{
    public class MemorySaverDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Chest> Chests { get; set; }

        public DbSet<File> Files { get; set; }

        public MemorySaverDBContext(DbContextOptions<MemorySaverDBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(p => p.OwnedChests)
                .WithOne(t => t.Owner)
                .HasForeignKey(k => k.OwnerId)
                .IsRequired(true);

            modelBuilder.Entity<Chest>()
                .HasMany(p => p.FilesInChest)
                .WithOne(f => f.Chest)
                .HasForeignKey(k => k.ChestId)
                .IsRequired(true);
        }
    }
}
