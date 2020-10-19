using System;
using System.Collections.Generic;
using System.Text;

namespace NetHacksPack.Database.Extension.EFCore.Logging
{
    public class IgnoredEntity
    {
        public IgnoredEntity(Type entityType, IEnumerable<string> ignoredLogsTypes)
        {
            EntityType = entityType;
            IgnoredLogsTypes = ignoredLogsTypes;
        }

        public Type EntityType { get; }
        public IEnumerable<string> IgnoredLogsTypes { get; }
    }
}
