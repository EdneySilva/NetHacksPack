using Microsoft.Extensions.DependencyInjection;
using System;
using MediatR;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace NetHacksPack.Core.Extensions.Events.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddObjectEventsAndMessagesHandler(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }
    }
}
