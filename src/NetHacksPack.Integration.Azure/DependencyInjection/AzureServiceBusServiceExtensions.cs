using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetHacksPack.Integration.Abstractions;
using NetHacksPack.Integration.Azure.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetHacksPack.Integration.Azure.DependencyInjection
{
    public static class AzureServiceBusServiceExtensions
    {
        public static IServiceCollection AddAzureServiceBus(this IServiceCollection services, IConfiguration configuration, Func<IConfiguration, IConfigurationSection> connectionStringProvider)
        {
            services.AddOptions();

            services.AddScoped<IEventBusManager, AzureServiceBusManager>();
            services.AddScoped<SubscriptionManager>();
            services.AddScoped<AzureServiceBusConsumer>();
            services.AddScoped<ServiceBusSenderPoolFactory>();
            services.AddScoped<AzureConnectionFactory>();
            services.Configure<AzureConnectionOptions>(connectionStringProvider(configuration));
            services.AddScoped<AzureServiceBusPublisher>();
            services.AddScoped<IEventBus, AzureServiceBus>();
            return services;
        }

        public static IServiceCollection AddEventBusErrorHandler<T>(this IServiceCollection services) where T : class, IEventBusErrorHandle
        {
            services.AddSingleton<T>();
            return services;
        }
    }
}
