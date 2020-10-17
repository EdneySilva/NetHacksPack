using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;

namespace NetHacksPack.Database.Events
{
    public class TrackedEntity
    {
        public TrackedEntity(IEnumerable<EntityEntry> entityEntries)
        {
            this.EntityEntries = entityEntries;
            this.OriginalValues = entityEntries.GroupBy(b => b.OriginalValues.EntityType.GetTableName()).ToDictionary(
                s => s.Key,
                s => s.Select(item => new Tracker(item.Metadata, item, item.OriginalValues.Clone()))
             );
        }

        public IEnumerable<EntityEntry> EntityEntries { get; }
        public Dictionary<string, IEnumerable<Tracker>> OriginalValues { get; }
    }
}
