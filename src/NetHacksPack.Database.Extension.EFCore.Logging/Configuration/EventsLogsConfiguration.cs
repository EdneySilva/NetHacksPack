using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetHacksPack.Database.Extension.EFCore.Logging.Configuration
{
    public delegate EventsLogsConfiguration EventLogsConfigurationProvider(IServiceProvider serviceProvider);

    public abstract class EventsLogsConfiguration : IEntityTypeConfiguration<EventLog>
    {
        public abstract void Configure(EntityTypeBuilder<EventLog> builder);
    }
}
