using ES.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ES.Domain
{
    public class CoreDBContext : DbContext
    {
        //public DbSet<Candle> Candles { get; set; }

        public DbSet<Exchange> Exchanges { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Pair> Pairs { get; set; }

        public CoreDBContext(DbContextOptions<CoreDBContext> options)
         : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exchange>(e =>
            {
                e.HasMany(x => x.Pairs);
            });

            modelBuilder.Entity<Currency>(e =>
            {
                e.HasMany<Pair>().WithOne(x => x.CurrencyTo).HasForeignKey(x => x.CurrencyToId);
                e.HasMany<Pair>().WithOne(x => x.CurrencyFrom).HasForeignKey(x => x.CurrencyFromId);
            });

            //modelBuilder.Entity<Pair>(e =>
            //{
               
            //});

        }
    }
}
