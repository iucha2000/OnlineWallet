using Microsoft.EntityFrameworkCore;
using OnlineWallet.Domain.Entities;

namespace OnlineWallet.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        DbSet<User> Users { get; set; }
        DbSet<Wallet> Wallets { get; set; }
        DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wallet>()
                        .HasMany(left => left.Transactions)
                        .WithMany(right => right.Wallets)
                        .UsingEntity(join => join.ToTable("WalletTransactions"));
        }
    }
}
