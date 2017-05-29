// Assembly           : Enexure.MicroBus.Sagas.Infrastructure
// Author              : Topsey
// Created On        : 28/5/2017
// Last Modified By : Ian
// Last Modified On : 28/5/2017 at 4:21 PM
// **************************************************************************
// <summary></summary>

using System;
using System.Linq;
using System.Threading.Tasks;
using Enexure.MicroBus.Sagas.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;

namespace Enexure.MicroBus.Sagas.Infrastructure.Context
{
    public class MicrobusSagaContext : DbContext, IMicrobusSagaContext, IDisposable
    {
        public MicrobusSagaContext(DbContextOptions<MicrobusSagaContext> options)
            : base(options)
        {
        }

        public DbSet<MicrobusSaga> MicrobusSagas { get; set; }

        public Task<int> SaveChangesAsync()
        {
            AddTimestamps();
            return base.SaveChangesAsync();
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is IModificationHistory && (x.State == EntityState.Added));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((IModificationHistory) entity.Entity).CreatedAt = DateTime.UtcNow;
                }
                ((IModificationHistory) entity.Entity).IsDirty = false;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}