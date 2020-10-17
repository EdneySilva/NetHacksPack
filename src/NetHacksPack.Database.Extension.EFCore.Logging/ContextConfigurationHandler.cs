using MediatR;
using NetHacksPack.Database.Events;
using NetHacksPack.Database.Extension.EFCore.Logging.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetHacksPack.Database.Extension.EFCore.Logging
{
    class ContextConfigurationHandler : IRequestHandler<ApplyConfigurationsToContextCommand, bool>
    {
        private readonly EventLogsConfigurationProvider eventLogsConfigurationProvider;
        private readonly IServiceProvider serviceProvider;

        public ContextConfigurationHandler(EventLogsConfigurationProvider eventLogsConfigurationProvider, IServiceProvider serviceProvider)
        {
            this.eventLogsConfigurationProvider = eventLogsConfigurationProvider;
            this.serviceProvider = serviceProvider;
        }

        public async Task<bool> Handle(ApplyConfigurationsToContextCommand request, CancellationToken cancellationToken)
        {
            request.UseConfiguration(eventLogsConfigurationProvider(serviceProvider));
            return true;
        }
    }
}
