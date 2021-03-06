﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetHacksPack.Database.Events;
using System;
using System.Security.Principal;

namespace NetHacksPack.Database.Extension.EFCore.Logging.DependencyInjection
{
    public delegate DbContext DbContextProvider(IServiceProvider service);

    public delegate IIdentity UserProvider(IServiceProvider service);

    public class LoggerExtensionBuilder
    {
        private readonly IServiceCollection services;

        internal LoggerExtensionBuilder(IServiceCollection services)
        {
            this.services = services;
        }

        public LoggerExtensionBuilder UseLogsOnInsert()
        {
            services
                .AddScoped<INotificationHandler<DataAddedEvent<TrackedEntity>>, LogEventHandler>();
            return this;
        }

        public LoggerExtensionBuilder UseLogsOnUpdate()
        {
            services
                .AddScoped<INotificationHandler<DataUpdatedEvent<TrackedEntity>>, LogEventHandler>();
            return this;
        }

        public LoggerExtensionBuilder UseLogsOnDelete()
        {
            services
                .AddScoped<INotificationHandler<DataDeletedEvent<TrackedEntity>>, LogEventHandler>();
            return this;
        }

        public IServiceCollection DisableLogs(Action<DisableLoggerExtensionBuilder> configure)
        {
            services.AddSingleton((serviceProvider) =>
            {
                var disabledLogger = new DisableLoggerExtensionBuilder();
                configure(disabledLogger);
                return disabledLogger.Build();
            });
            return services;
        }
    }
}
