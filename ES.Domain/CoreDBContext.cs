using ES.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ES.Domain
{
    public class CoreDBContext : DbContext
    {
        public DbSet<Exchange> Exchanges { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<ExchangePair> Pairs { get; set; }
        public DbSet<Candle> Candles { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        public CoreDBContext(DbContextOptions<CoreDBContext> options)
         : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exchange>(e =>
            {
                e.HasAlternateKey(e => e.Name);
                e.HasMany(x => x.Pairs).WithOne(x => x.Exchange).HasForeignKey(x => x.ExchangeId);
            });

            modelBuilder.Entity<Currency>(e =>
            {
                e.HasAlternateKey(e => e.Symbol);
                e.HasMany<ExchangePair>().WithOne(x => x.CurrencyTo).HasForeignKey(x => x.CurrencyToId);
                e.HasMany<ExchangePair>().WithOne(x => x.CurrencyFrom).HasForeignKey(x => x.CurrencyFromId);
            });

            modelBuilder.Entity<ExchangePair>(e =>
            {
                e.HasAlternateKey(e => new { e.ExchangeId, e.CurrencyToId, e.CurrencyFromId });
                e.ToTable(nameof(Pairs));
                e.HasMany<Candle>();
            });

            modelBuilder.Entity<Candle>(e =>
            {
                e.HasAlternateKey(e => new { e.TimeOpen, e.TimeClose, e.PairId });
            });

            modelBuilder.Entity<Subscription>(e =>
            {
                e.HasMany(x => x.Currencies);
            });

            modelBuilder.Entity<Account>(e =>
            {
                e.HasMany(x => x.Subscriptions);
            });
        }
    }
}
