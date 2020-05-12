using ES.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ES.Domain
{
    public class CoreDBContext : DbContext
    {
        public DbSet<Exchange> Exchanges { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<ExchangePair> Pairs { get; set; }

        public CoreDBContext(DbContextOptions<CoreDBContext> options)
         : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exchange>(e =>
            {
                e.HasMany(x => x.Pairs).WithOne(x => x.Exchange).HasForeignKey(x => x.ExchangeId);
                e.HasAlternateKey(e => e.Name);
            });

            modelBuilder.Entity<Currency>(e =>
            {
                e.HasMany<ExchangePair>().WithOne(x => x.CurrencyTo).HasForeignKey(x => x.CurrencyToId);
                e.HasMany<ExchangePair>().WithOne(x => x.CurrencyFrom).HasForeignKey(x => x.CurrencyFromId);
                e.HasAlternateKey(e => e.Symbol);
            });

            modelBuilder.Entity<ExchangePair>(e =>
            {
                e.ToTable(nameof(Pairs));
            });

        }
    }
}
