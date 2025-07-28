namespace UsernameService.Api.Data;

using Microsoft.EntityFrameworkCore;
using UsernameService.Api.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
                    .HasIndex(u => u.Username)
                    .IsUnique();
    }
}
