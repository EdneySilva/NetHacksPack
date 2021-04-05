using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;

namespace NetHacksPack.Database.Events
{
    public struct Tracker
    {
        public Tracker(IEntityType entityType, EntityEntry entry, Dictionary<string, string> propertyValues) : this()
        {
            this.Metadata = entityType;
            this.Entry = entry;
            this.OriginalValues = propertyValues;
        }

        public IEntityType Metadata { get; set; }
        public EntityEntry Entry { get; set; }
        public Dictionary<string, string> OriginalValues { get; set; }
    }
}
