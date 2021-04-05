using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetHacksPack.Database.Extension.EFCore.Logging;
using NetHacksPack.Database.Extension.EFCore.Logging.Configuration;

namespace NetHacksPack.Database.Extensions.EFCore.Logging.PostgreSql.Configuration
{
    internal class PostgreSqlEventLogConfiguration : EventsLogsConfiguration
    {
        private readonly string tableName;

        public PostgreSqlEventLogConfiguration(string tableName)
        {
            this.tableName = tableName;
        }

        public override void Configure(EntityTypeBuilder<EventLog> builder)
        {
            builder.ToTable(tableName);

            builder.HasKey(p => p.Id);

            builder
                .Property(p => p.Id)
                .HasColumnType("UUID")
                .ValueGeneratedOnAdd();
            builder
                .Property(p => p.CreatedAt)
                .HasColumnType("TIMESTAMP");
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
                .HasColumnType("JSONB");
            builder
                .Property(p => p.OriginalValues)
                .HasColumnType("JSONB");
        }
    }
}
