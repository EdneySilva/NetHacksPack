using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using NetHacksPack.Core;
using NetHacksPack.Core.Extensions.Events;
using NetHacksPack.Database.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NetHacksPack.Database.Extension.EF
{
    public abstract class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private readonly IMediatorHandler mediatorHandler;
        private readonly IServiceProvider serviceProvider;

        public DbContext()
        {

        }

        public DbContext(IMediatorHandler mediatorHandler, IServiceProvider serviceProvider)
        {
            this.mediatorHandler = mediatorHandler;
            this.serviceProvider = serviceProvider;
        }

        public DbContext(IMediatorHandler mediatorHandler, IServiceProvider serviceProvider, DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
            this.mediatorHandler = mediatorHandler;
            this.serviceProvider = serviceProvider;
        }

        protected abstract void ConfigureModels(ModelBuilder modelBuilder);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            this.mediatorHandler?.SendCommand(new ApplyConfigurationsToContextCommand(modelBuilder));
            this.ConfigureModels(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var events = this.GetTrackedEvents();
            var result = base.SaveChanges(acceptAllChangesOnSuccess);
            if (result > 0 && this.mediatorHandler != null)
            {
                foreach (var evento in events)
                    Task.WaitAll(this.mediatorHandler?.PublishEvent(evento));
            }
            return result;
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var events = this.GetTrackedEvents();
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            if (result > 0 && this.mediatorHandler != null)
            {
                foreach (var evento in events)
                    Task.WaitAll(this.mediatorHandler?.PublishEvent(evento));
            }
            return result;
        }


        private List<ObjectEvent> GetTrackedEvents()
        {
            var entries = this.ChangeTracker.Entries();
            List<ObjectEvent> events = new List<ObjectEvent>(entries.Count());
            foreach (var item in entries.GroupBy(g => g.State))
            {
                if(item.Key == EntityState.Added)
                    events.Add(new DataAddedEvent<TrackedEntity>(new TrackedEntity(item.ToList())));
                else if (item.Key == EntityState.Deleted)
                    events.Add(new DataDeletedEvent<TrackedEntity>(new TrackedEntity(item.ToList())));
                else if (item.Key == EntityState.Modified)
                    events.Add(new DataUpdatedEvent<TrackedEntity>(new TrackedEntity(item.ToList())));
            }
            return events;
        }
    }
}
