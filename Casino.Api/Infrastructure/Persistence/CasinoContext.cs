using Casino.Api.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Casino.Api.Infrastructure.Persistence
{
    public class CasinoContext : IdentityDbContext<Player>
    {
        public CasinoContext(DbContextOptions<CasinoContext> options) : base(options) { 
        
        }
        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Player { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
    }
}
