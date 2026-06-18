using Microsoft.EntityFrameworkCore;
using projeto_ejacast.Models;

namespace projeto_ejacast.config;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<ContadorVisita> ContadorVisitas { get; set; }
}