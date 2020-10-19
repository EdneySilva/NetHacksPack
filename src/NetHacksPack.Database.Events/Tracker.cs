using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NetHacksPack.Database.Events
{
    public struct Tracker
    {
        public Tracker(IEntityType entityType, EntityEntry entry, PropertyValues propertyValues) : this()
        {
            this.Metadata = entityType;
            this.Entry = entry;
            this.OriginalValues = propertyValues;
        }

        public IEntityType Metadata { get; set; }
        public EntityEntry Entry { get; set; }
        public PropertyValues OriginalValues { get; set; }
    }
}
