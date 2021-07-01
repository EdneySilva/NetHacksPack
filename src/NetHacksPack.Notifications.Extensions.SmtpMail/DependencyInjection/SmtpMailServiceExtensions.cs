using Microsoft.Extensions.DependencyInjection;
using NetHacksPack.Notifications.Abstractions.Notificators;
using NetHacksPack.Notifications.Abstractions.Providers;
using NetHacksPack.Notifications.Extensions.SmtpMail.Provider;
using System;

namespace NetHacksPack.Notifications.Extensions.SmtpMail.DependencyInjection
{
    /// <summary>
    /// Extension class used to configure the required interfaces from this package
    /// </summary>
    public static class SmtpMailServiceExtensions
    {
        /// <summary>
        /// Add all the base interfaces used to send smpt mail notifications
        /// </summary>
        /// <param name="services">the container to configure the interfaces</param>
        /// <param name="configureSmtpMailService">an action that configures the smpt configurations</param>
        /// <returns></returns>
        public static IServiceCollection AddSmtpMailNotifications(this IServiceCollection services, Action<SmtpMailOptions> configureSmtpMailService)
        {
            services.AddScoped<SmtpEmailNotificatorClient>();
            services.AddScoped<INotificator, SmtpEmailNotificatorClient>();
            services.AddScoped<INotificatorResolver, EmailResolver>();
            services.Configure(configureSmtpMailService);
            return services;
        }
    }
}
