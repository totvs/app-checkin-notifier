using App.CheckIn.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Tnf.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace App.CheckIn.EntityFrameworkCore
{
    public class AppCheckInDbContext : TnfDbContext
    {
        public DbSet<EventSubscription> Subscriptions { get; set; }

        public AppCheckInDbContext(DbContextOptions<AppCheckInDbContext> options, ITnfSession session)
            : base(options, session)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EventSubscription>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.Property(s => s.Id)
                    .HasValueGenerator<GuidValueGenerator>()
                    .ValueGeneratedOnAdd();
            });
        }
    }
}
