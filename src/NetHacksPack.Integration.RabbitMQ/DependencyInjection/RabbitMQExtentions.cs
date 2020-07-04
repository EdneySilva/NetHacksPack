using NetHacksPack.Hosting.Abstractions.Providers;
using NetHacksPack.Integration.Abstractions;
using NetHacksPack.Integration.RabbitMQ;
using NetHacksPack.Integration.RabbitMQ.Handler;
using NetHacksPack.Integration.RabbitMQ.IO;
using NetHacksPack.Integration.RabbitMQ.Providers;
using Microsoft.Extensions.Logging;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RabbitMQExtentions
    {
        public static IServiceCollection AddRabbitMessageSerializer(this IServiceCollection services)
        {
            return services.AddSingleton<RabbitMessageSerializer>(MessageWriter.EventStrigify);
        }

        public static IServiceCollection AddRabbitMessageReader(this IServiceCollection services, MessageReaderProvider messageReaderProvider)
        {
            services.AddScoped((services) => messageReaderProvider);
            return services;
        }

        public static IServiceCollection AddRabbitMessageReader(this IServiceCollection services)
        {
            services.AddScoped((serviceProvider) =>
            {
                return new MessageReaderProvider((eventArgs) =>
                {
                    return new MessageReader(eventArgs, serviceProvider.GetService<ILogger<MessageReader>>());
                });
            });
            return services;
        }

        public static IServiceCollection AddRabbitMessageWriter(this IServiceCollection services)
        {
            services.AddScoped((serviceProvider) =>
            {
                return new MessageWriterProvider((specifications) =>
                {
                    return new MessageWriter(specifications);
                });
            });
            return services;
        }

        public static IServiceCollection AddRabbitConnectionProvider(this IServiceCollection services, Action<ConnectionOptions> configConnection)
        {
            return services
                    .AddSingleton(configConnection);
        }

        public static IServiceCollection AddRabbitConnectionProvider(this IServiceCollection services)
        {
            services
                .AddScoped<IConnectionProvider<RabbitMQConnection>>((serviceProvider) =>
                {
                    var connectionStringProvider = serviceProvider.GetService<IConnectionStringProvider>();
                    var defaultConneciton = serviceProvider.GetService<ConnectionOptions>();
                    var connectionInfoConfigurator = serviceProvider.GetService<Action<ConnectionOptions>>();
                    connectionInfoConfigurator?.Invoke(defaultConneciton);
                    var result = new RabbitMQConnectionProvider(connectionStringProvider, defaultConneciton, serviceProvider.GetService<ILogger<RabbitMQConnection>>());
                    return result;
                });
            return services;
        }

        public static IServiceCollection AddRabbitSubscription(this IServiceCollection services)
        {
            services.AddScoped<ISubscriptionManager<RabbitContext>, RabbitMQSubscriptionManager>();
            return services;
        }

        public static IServiceCollection AddRabbitPublisher(this IServiceCollection servics)
        {
            return servics.AddScoped<IEventBusPublisher, RabbitMQPublisher>();
        }

        public static IServiceCollection ConfigurePipeline(this IServiceCollection services, Action<RabbitPipeline> pipeLineConfigurator)
        {
            services.AddSingleton(pipeLineConfigurator);
            return services;
        }


        public static IServiceCollection AddRabbitEventBus(this IServiceCollection services, Func<IConnectionStringProvider, ConnectionOptions> connectionProvider)
        {
            services.AddSingleton<RabbitPipeline>((service) =>
            {
                return new RabbitPipeline(service.GetService<Action<RabbitPipeline>>() ?? new Action<RabbitPipeline>((item) => { }));
            });
            services.AddSingleton(connectionProvider);
            services.AddSingleton((serviceProvider) =>
            {
                var connectionStringProvider = serviceProvider.GetService<IConnectionStringProvider>();
                return serviceProvider
                    .GetService<Func<IConnectionStringProvider, ConnectionOptions>>()
                    .Invoke(connectionStringProvider);
            });
            services.AddScoped<IEventBusListener<RabbitContext>, RabbitMQListener>();
            services.AddScoped<IEventBus, RabbitMQEventBus>((serviceProvider) =>
            {
                return new RabbitMQEventBus(
                    serviceProvider.GetRequiredService<ISubscriptionManager<RabbitContext>>(),
                    serviceProvider.GetRequiredService<IEventBusListener<RabbitContext>>(),
                    serviceProvider.GetRequiredService<IEventBusPublisher>()
                );
            });
            return services;
        }

        public static IServiceCollection AddRabbitMQIntegration(this IServiceCollection services, Func<IConnectionStringProvider, ConnectionOptions> connectionProvider)
        {
            return services
                    .AddRabbitMessageReader()
                    .AddRabbitMessageWriter()
                    .AddRabbitConnectionProvider()
                    .AddRabbitSubscription()
                    .AddRabbitPublisher()
                    .AddRabbitEventBus(connectionProvider);
        }
    }
}