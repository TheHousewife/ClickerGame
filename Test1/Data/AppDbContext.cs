using Microsoft.EntityFrameworkCore;

namespace Test1.Data;

public class AppDbContext : DbContext
{
    public DbSet<PlayerData> Players { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
