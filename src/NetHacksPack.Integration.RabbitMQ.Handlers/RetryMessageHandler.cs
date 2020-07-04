using NetHacksPack.Integration.RabbitMQ.Handler;
using System;
using System.Threading.Tasks;

namespace NetHacksPack.Integration.RabbitMQ.Handlers
{
    public class RetryMessageHandler : IRabbitMessageHandler
    {
        public Task HandlePublish(RabbitContext rabbitContext, RabbitMessageHandler next)
        {
            return next();
        }

        public Task HandleReceive(RabbitContext rabbitContext, RabbitMessageHandler next)
        {
            try
            {
                //rabbitContext.Serializer(null);
                //rabbitContext.ReceivedMessage.Body 
                //rabbitContext.ReceivedMessage.Body = new []
                return next();
            }
            catch (Exception)
            {
                var properties = rabbitContext.BasicPropertyCreator();
                rabbitContext.WriteMessageToPublish(rabbitContext.ReceivedMessage.Body, properties);
                return Task.CompletedTask;
            }
        }
    }
}
