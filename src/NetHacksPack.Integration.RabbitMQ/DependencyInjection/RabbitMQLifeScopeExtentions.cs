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
    public static class RabbitMQLifeScopeExtentions
    {
        public static IServiceCollection AddRabbitMessageReader(this IServiceCollection services, MessageReaderProvider messageReaderProvider, ServiceLifetime lifetime)
        {
            var serviceDescriptor = new ServiceDescriptor(typeof(MessageReaderProvider), (services) => messageReaderProvider, lifetime);
            services.Add(serviceDescriptor);
            return services;
        }

        public static IServiceCollection AddRabbitMessageReader(this IServiceCollection services, ServiceLifetime lifetime)
        {
            services.Add(
                new ServiceDescriptor(
                    typeof(MessageReaderProvider),
                    (serviceProvider) => {
                        return new MessageReaderProvider((eventArgs) => {
                            return new MessageReader(eventArgs, serviceProvider.GetService<ILogger<MessageReader>>());
                        });
                    },lifetime));
            return services;
        }

        public static IServiceCollection AddRabbitMessageWriter(this IServiceCollection services, ServiceLifetime lifetime)
        {
            services.Add(
                new ServiceDescriptor(
                    typeof(MessageWriterProvider),
                    (serviceProvider) => {

                        return new MessageWriterProvider((specifications) =>
                        {
                            return new MessageWriter(specifications);
                        });
                    }, lifetime));
            return services;
        }

        public static IServiceCollection AddRabbitConnectionProvider(this IServiceCollection services, Action<ConnectionOptions> configConnection, ServiceLifetime lifetime)
        {
            services.Add(new ServiceDescriptor(
                        typeof(Action<ConnectionOptions>),
                        (service) => configConnection,
                        lifetime)
                     );
            return services;
        }

        public static IServiceCollection AddRabbitConnectionProvider(this IServiceCollection services, ServiceLifetime lifetime)
        {
            services
                .Add(
                    new ServiceDescriptor(
                        typeof(IConnectionProvider<RabbitMQConnection>), 
                        (serviceProvider) => {
                            var connectionStringProvider = serviceProvider.GetService<IConnectionStringProvider>();
                            var defaultConneciton = serviceProvider.GetService<ConnectionOptions>();
                            var connectionInfoConfigurator = serviceProvider.GetService<Action<ConnectionOptions>>();
                            connectionInfoConfigurator?.Invoke(defaultConneciton);
                            var result = new RabbitMQConnectionProvider(connectionStringProvider, defaultConneciton, serviceProvider.GetService<ILogger<RabbitMQConnection>>());
                            return result;
                        }, lifetime)
                );
            return services;
        }

        public static IServiceCollection AddRabbitSubscription(this IServiceCollection services, ServiceLifetime lifetime)
        {
            var serviceDescriptor = new ServiceDescriptor(typeof(ISubscriptionManager<RabbitContext>), typeof(RabbitMQSubscriptionManager), lifetime);
            services.Add(serviceDescriptor);
            return services;
        }

        public static IServiceCollection AddRabbitPublisher(this IServiceCollection services, ServiceLifetime lifetime)
        {
            services.Add(new ServiceDescriptor(typeof(IEventBusPublisher), typeof(RabbitMQPublisher), lifetime));
            return services;
        }

        public static IServiceCollection AddRabbitEventBus(this IServiceCollection services, string busName, Func<IConnectionStringProvider, ConnectionOptions> connectionProvider, ServiceLifetime lifetime)
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
            services.Add(new ServiceDescriptor(typeof(IEventBusListener<RabbitContext>), typeof(RabbitMQListener), lifetime));
            services.Add(new ServiceDescriptor(typeof(IEventBus), (serviceProvider) =>
            {
                return new RabbitMQEventBus(
                    serviceProvider.GetRequiredService<ISubscriptionManager<RabbitContext>>(),
                    serviceProvider.GetRequiredService<IEventBusListener<RabbitContext>>(),
                    serviceProvider.GetRequiredService<IEventBusPublisher>()
                );
            }, lifetime));
            return services;
        }

        public static IServiceCollection AddRabbitMQIntegration(this IServiceCollection services, string busName, Func<IConnectionStringProvider, ConnectionOptions> connectionProvider, ServiceLifetime lifetime)
        {
            return services
                .AddRabbitMessageSerializer()
                .AddRabbitMessageReader(lifetime)
                .AddRabbitMessageWriter(lifetime)
                .AddRabbitConnectionProvider(lifetime)
                .AddRabbitSubscription(lifetime)
                .AddRabbitPublisher(lifetime)
                .AddRabbitEventBus(busName, connectionProvider, lifetime);
        }
    }
}
