using Microservice.Payment.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Payment.Api.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Product> Products { get; set; }
}