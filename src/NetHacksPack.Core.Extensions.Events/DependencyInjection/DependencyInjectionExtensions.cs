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
        public static IServiceCollection AddObjectEventsAndMessagesHandler(this IServiceCollection services, Type classInAssembly)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddMediatR(classInAssembly);
            return services;
        }
    }
}
