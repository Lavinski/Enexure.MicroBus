// Assembly           : WZHi.Hangfire.Microbus.Extensions
// Author              : Topsey
// Created On        : 22/5/2017
// Last Modified By : Ian
// Last Modified On : 24/5/2017 at 2:36 PM
// **************************************************************************
// <summary>Based on Derek Comartin article Background Commands with MediatR and Hangfire</summary>

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WZHi.Hangfire.Microbus.Integration.Model;

namespace WZHi.Hangfire.Microbus.Integration.Infrastructure
{
    public class HangfireMicrobusContext : DbContext, IHangfireMicrobusContext, IDisposable
    {
        public HangfireMicrobusContext(DbContextOptions<HangfireMicrobusContext> options)
            : base(options)
        {
        }

        public DbSet<MicrobusRequest> MicrobusRequests { get; set; }

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