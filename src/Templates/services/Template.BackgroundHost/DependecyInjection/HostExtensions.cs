using NetHacksPack.Integration.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Template.BackgroundHost.DependecyInjection
{
    public static class HostExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddAssemblyNameAsApplicationId();
            services
                .AddConnectionProvider();

            services
                .AddRabbitMQIntegration("realtime-broker",
                    (connectionProvider) => connectionProvider.GetConnectorOptions<ConnectionOptions>("RabbitMQConnectionOptions")
                , ServiceLifetime.Singleton);

            services
                .AddEventBusListenerHostedService<Services.ExampleHostService>();

            services
                .AddEventHandler<Events.Handlers.IUserCreatedEventHandler, Events.Handlers.UserCreatedEventHandler>()
                ;
            var handler = new NetHacksPack.Integration.RabbitMQ.Handlers.RetryMessageHandler();
            services.ConfigurePipeline((rabbitPipeline) =>
            {
                rabbitPipeline.ConfigureEvent<Events.UserCreated>((options) =>
                {
                    options.Use(handler);
                });
            });

            return services;
        }
    }
}
