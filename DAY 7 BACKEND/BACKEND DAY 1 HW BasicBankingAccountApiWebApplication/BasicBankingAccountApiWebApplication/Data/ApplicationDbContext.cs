
using BasicBankingAccountApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicBankingAccountApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<BankAccount> BankAccounts => Set<BankAccount>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BankAccount>(entity =>
        {
            entity.HasIndex(a => a.AccountNumber)
                .IsUnique();

            entity.Property(a => a.Balance)
                .HasPrecision(18, 2);
        });
    }
}
