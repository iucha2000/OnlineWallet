using Microsoft.EntityFrameworkCore;
using OnlineWallet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineWallet.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        DbSet<User> Users { get; set; }
        DbSet<Wallet> Wallets { get; set; }
        DbSet<Transaction> Transactions { get; set; }
    }
}
