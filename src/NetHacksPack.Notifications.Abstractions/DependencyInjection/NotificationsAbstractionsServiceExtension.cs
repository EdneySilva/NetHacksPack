using Microsoft.Extensions.DependencyInjection;
using NetHacksPack.Notifications.Abstractions.Providers;

namespace NetHacksPack.Notifications.Abstractions.DependencyInjection
{
    /// <summary>
    /// Extension class used to configure the container with the required dependencies to use notifications
    /// </summary>
    public static class NotificationsAbstractionsServiceExtension
    {
        /// <summary>
        /// Add the base interfaces into the dependency injection container
        /// </summary>
        /// <param name="services">collection from the container</param>
        /// <returns>the same collection with new dependencies configured</returns>
        public static IServiceCollection AddNotifications(this IServiceCollection services)
        {
            services.AddScoped<INotificationClient, NotificationClient>();
            services.AddScoped<INotificatorProvider, NotificatorProvider>();
            return services;
        }
    }
}
