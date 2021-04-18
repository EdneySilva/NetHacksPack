using System;
using System.Collections.Generic;
using System.Text;

namespace NetHacksPack.Database.Extension.EFCore.Logging
{
    public class IgnoredEntity
    {
        public IgnoredEntity(Type entityType, ICollection<string> ignoredLogsTypes)
        {
            EntityType = entityType;
            IgnoredLogsTypes = ignoredLogsTypes;
        }

        public Type EntityType { get; }
        internal ICollection<string> IgnoredLogsTypes { get; }
    }
}
