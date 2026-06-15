using Microsoft.EntityFrameworkCore;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<netCoreWebApi.Models.Contact> Contact { get; set; } = default!;
}
