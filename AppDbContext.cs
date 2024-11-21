using chatApi.Model;
using Microsoft.EntityFrameworkCore;

namespace chatApi;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Define your tables here
    public DbSet<Messages> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure your entity models if necessary
        modelBuilder.Entity<Messages>().ToTable("Messages");
    }
}