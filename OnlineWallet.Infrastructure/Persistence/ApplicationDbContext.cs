using Microsoft.EntityFrameworkCore;
using OnlineWallet.Domain.Entities;
using OnlineWallet.Domain.Enums;
using OnlineWallet.Infrastructure.Handlers;

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


            //Seeding data
            Guid id = Guid.Parse("88e2537d-59fa-4ea4-88a0-4da67160f69f");

            var password = "Aa123123";
            var hasher = new PasswordHandler();
            hasher.CreateHashAndSalt(password, out var passwordHash, out var passwordSalt);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = id,
                    FirstName = "Iuri",
                    LastName = "Megreladze",
                    Email = "imegr@gmail.com",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Role = Role.Admin,
                    Wallets = new List<Wallet>()
                });
        }
    }
}
