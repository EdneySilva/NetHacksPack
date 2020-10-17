using Microsoft.EntityFrameworkCore;
using NetHacksPack.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetHacksPack.Database.Events
{
    public class ApplyConfigurationsToContextCommand : Command
    {
        private readonly ModelBuilder modelBuilder;

        public ApplyConfigurationsToContextCommand(ModelBuilder modelBuilder)
            : base()
        {
            this.modelBuilder = modelBuilder;
        }

        protected ApplyConfigurationsToContextCommand()
        {
        }

        public void UseConfiguration<TEntity>(IEntityTypeConfiguration<TEntity> instance) where TEntity : class
        {
            this.modelBuilder.ApplyConfiguration(instance);
        }
    }
}
