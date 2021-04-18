using NetHacksPack.Database.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetHacksPack.Database.Extension.EFCore.Logging.DependencyInjection
{
    public class DisableLoggerExtensionBuilder
    {
        private Dictionary<string, IgnoredEntity> data = new Dictionary<string, IgnoredEntity>();

        public DisableLoggerExtensionBuilder()
        {
        }

        public void DisableInsertTo<TEntity>()
        {
            var type = typeof(TEntity).Name;
            if (!data.ContainsKey(type))
                data.Add(type, new IgnoredEntity(typeof(TEntity), new List<string>()));
            if (!data[type].IgnoredLogsTypes.Contains(typeof(DataDeletedEvent<>).Name))
                data[type].IgnoredLogsTypes.Add(typeof(DataAddedEvent<>).Name);
        }

        public void DisableUpdateTo<TEntity>()
        {
            var type = typeof(TEntity).Name;
            if (!data.ContainsKey(type))
                data.Add(type, new IgnoredEntity(typeof(TEntity), new List<string>()));
            if (!data[type].IgnoredLogsTypes.Contains(typeof(DataDeletedEvent<>).Name))
                data[type].IgnoredLogsTypes.Add(typeof(DataUpdatedEvent<>).Name);
        }

        public void DisableDeleteTo<TEntity>()
        {
            var type = typeof(TEntity).Name;
            if (!data.ContainsKey(type))
                data.Add(type, new IgnoredEntity(typeof(TEntity), new List<string>()));
            if (!data[type].IgnoredLogsTypes.Contains(typeof(DataDeletedEvent<>).Name))
                data[type].IgnoredLogsTypes.Add(typeof(DataDeletedEvent<>).Name);
        }

        public void DisableAllTo<TEntity>()
        {
            this.DisableInsertTo<TEntity>();
            this.DisableUpdateTo<TEntity>();
            this.DisableDeleteTo<TEntity>();
        }

        internal IEnumerable<IgnoredEntity> Build()
        {
            return data.Select(s => s.Value).AsEnumerable();
        }
    }
}
