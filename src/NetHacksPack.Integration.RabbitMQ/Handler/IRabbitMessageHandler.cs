using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetHacksPack.Integration.RabbitMQ.Handler
{
    public delegate Task RabbitMessageHandler();
    public delegate Task RabbitMessageHandlerMethod(RabbitContext rabbitHandlerContext, RabbitMessageHandler next);

    public interface IRabbitMessageHandler
    {
        public Task HandleReceive(RabbitContext rabbitContext, RabbitMessageHandler next);

        public Task HandlePublish(RabbitContext rabbitContext, RabbitMessageHandler next);
    }
}
