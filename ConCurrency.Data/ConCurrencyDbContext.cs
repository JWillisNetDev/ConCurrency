using ConCurrency.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace ConCurrency.Data;

public class ConCurrencyDbContext : DbContext
{
    public ConCurrencyDbContext()
    {
    }

    public ConCurrencyDbContext(DbContextOptions<ConCurrencyDbContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConCurrencyDbContext).Assembly);
    }
}
