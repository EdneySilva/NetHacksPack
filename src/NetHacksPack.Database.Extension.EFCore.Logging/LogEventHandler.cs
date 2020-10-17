using MediatR;
using Microsoft.EntityFrameworkCore;
using NetHacksPack.Database.Events;
using NetHacksPack.Database.Extension.EF;
using NetHacksPack.Database.Extension.EFCore.Logging.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetHacksPack.Database.Extension.EFCore.Logging
{
    public class LogEventHandler
        : INotificationHandler<DataAddedEvent<TrackedEntity>>
        , INotificationHandler<DataUpdatedEvent<TrackedEntity>>
        , INotificationHandler<DataDeletedEvent<TrackedEntity>>
    {
        private readonly IIdentity userAcessor;
        private readonly NetHacksPack.Database.Extension.EF.DbContext dbContext;
        private readonly IEnumerable<IgnoredEntity> listaNegras;

        public string LogType { get; }

        public LogEventHandler(
            UserProvider userAcessor, 
            IServiceProvider serviceProvider, 
            NetHacksPack.Database.Extension.EF.DbContext dbContext, 
            IEnumerable<IgnoredEntity> listaNegras)
        {
            this.userAcessor = userAcessor(serviceProvider);
            this.dbContext = dbContext;
            this.listaNegras = listaNegras;
            this.LogType = typeof(EventLog).Name;
        }

        public async Task Handle(DataUpdatedEvent<TrackedEntity> notification, CancellationToken cancellationToken)
        {

            foreach (var item in notification.Data.OriginalValues.SelectMany(k => k.Value.Select(s => new { Value = s, k.Key })))
            {
                if (IsOnBlackList(notification.GetType().Name, item.Value.OriginalValues.EntityType.ShortName()))
                    continue;
                var f = item.Value.Entry;
                var primaryKey = item.Value.Metadata.FindPrimaryKey();
                var primaryKeys = primaryKey.Properties.ToDictionary(p => p.Name, p => f.Property(p.Name).CurrentValue?.ToString());
                var originalItems = item.Value.OriginalValues.Properties.ToDictionary(p => p.Name, p => item.Value.OriginalValues.GetValue<object>(p)?.ToString());
                var currentValues = item.Value.Entry.CurrentValues.Properties.ToDictionary(p => p.Name, p => f.Property(p.Name).OriginalValue?.ToString());
                var log = new EventLog();
                log.CreatedAt = DateTime.Now;
                log.EventUser = userAcessor.Name;
                log.DataKeysValues = JsonConvert.SerializeObject(primaryKeys);
                log.EntityType = item.Key;
                log.EventType = notification.Type;
                log.OriginalValues = JsonConvert.SerializeObject(
                    originalItems
                        .Where(original =>
                            currentValues.Any(current => current.Key == original.Key && current.Value != original.Value)
                        ).ToDictionary(s => s.Key, s => s.Value)
                );
                this.dbContext.Add(log);
                await this.dbContext.SaveChangesAsync();
            }
        }

        public async Task Handle(DataDeletedEvent<TrackedEntity> notification, CancellationToken cancellationToken)
        {
            foreach (var item in notification.Data.OriginalValues.SelectMany(k => k.Value.Select(s => new { Value = s, k.Key })))
            {

                if (IsOnBlackList(notification.GetType().Name, item.Value.OriginalValues.EntityType.ShortName()))
                    continue;
                var f = item.Value.Entry;
                var primaryKey = item.Value.Metadata.FindPrimaryKey();
                var primaryKeys = primaryKey.Properties.ToDictionary(p => p.Name, p => f.Property(p.Name).CurrentValue?.ToString());
                var originalItems = item.Value.OriginalValues.Properties.ToDictionary(p => p.Name, p => item.Value.OriginalValues.GetValue<object>(p)?.ToString());
                var currentValues = item.Value.Entry.CurrentValues.Properties.ToDictionary(p => p.Name, p => f.Property(p.Name).OriginalValue?.ToString());
                var log = new EventLog();
                log.CreatedAt = DateTime.Now;
                log.EventUser = userAcessor.Name;
                log.DataKeysValues = JsonConvert.SerializeObject(primaryKeys);
                log.EntityType = item.Key;
                log.EventType = notification.Type;
                log.OriginalValues = JsonConvert.SerializeObject(originalItems);
                this.dbContext.Add(log);
                await this.dbContext.SaveChangesAsync();
            }
        }

        public async Task Handle(DataAddedEvent<TrackedEntity> notification, CancellationToken cancellationToken)
        {
            foreach (var item in notification.Data.OriginalValues.SelectMany(k => k.Value.Select(s => new { Value = s, k.Key })))
            {
                if (IsOnBlackList(notification.GetType().Name, item.Value.OriginalValues.EntityType.ShortName()))
                    continue;
                var f = item.Value.Entry;
                var primaryKey = item.Value.Metadata.FindPrimaryKey();
                var primaryKeys = primaryKey.Properties.ToDictionary(p => p.Name, p => f.Property(p.Name).CurrentValue?.ToString());
                var originalItems = item.Value.OriginalValues.Properties.ToDictionary(p => p.Name, p => item.Value.OriginalValues.GetValue<object>(p)?.ToString());
                var currentValues = item.Value.Entry.CurrentValues.Properties.ToDictionary(p => p.Name, p => f.Property(p.Name).OriginalValue?.ToString());
                var log = new EventLog();
                log.CreatedAt = DateTime.Now;
                log.EventUser = userAcessor.Name;
                log.DataKeysValues = JsonConvert.SerializeObject(primaryKeys);
                log.EntityType = item.Key;
                log.EventType = notification.Type;
                log.OriginalValues = JsonConvert.SerializeObject(originalItems);
                this.dbContext.Add(log);
                await this.dbContext.SaveChangesAsync();
            }
        }

        private bool IsOnBlackList(string type, string entityType)
        {
            return listaNegras.Any(itemIgnorado =>
                itemIgnorado.IgnoredLogsTypes.Contains(type) &&
                entityType == itemIgnorado.EntityType.Name
             );
        }
    }
}
