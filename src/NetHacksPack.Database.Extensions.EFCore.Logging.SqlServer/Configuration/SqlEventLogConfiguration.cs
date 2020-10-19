using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetHacksPack.Database.Extension.EFCore.Logging;
using NetHacksPack.Database.Extension.EFCore.Logging.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetHacksPack.Database.Extensions.EFCore.Logging.SqlServer.Configuration
{
    internal class SqlEventLogConfiguration : EventsLogsConfiguration
    {
        public override void Configure(EntityTypeBuilder<EventLog> builder)
        {
            builder.ToTable("EventsLogs");

            builder.HasKey(p => p.Id);

            builder
                .Property(p => p.Id)
                .HasColumnType("UNIQUEIDENTIFIER")
                .ValueGeneratedOnAdd();
            builder
                .Property(p => p.CreatedAt)
                .HasColumnType("DATETIME");
            builder
                .Property(p => p.EntityType)
                .HasColumnType("VARCHAR(100)");
            builder
                .Property(p => p.EventUser)
                .HasColumnType("VARCHAR(100)");
            builder
                .Property(p => p.EventType)
                .HasColumnType("VARCHAR(50)");
            builder
                .Property(p => p.DataKeysValues)
                .HasColumnType("JSON");
            builder
                .Property(p => p.OriginalValues)
                .HasColumnType("JSON");
        }
    }
}
