using NetHacksPack.Integration.Abstractions;
using System.Threading.Tasks;

namespace NetHacksPack.Integration.RabbitMQ.Handler
{
    class ExecutionPipe
    {
        private readonly RabbitMessageHandlerMethod[] methods;

        public ExecutionPipe(RabbitMessageHandlerMethod[] methods)
        {
            this.methods = methods;
        }

        public Task Execute(RabbitContext rabbitContext, EventMessage eventMessage)
        {
            RabbitMessageHandler next = null;
            int counter = 0;
            var currentMethods = methods;
            next = () =>
            {
                if (counter < currentMethods.Length)
                {
                    return currentMethods[counter++].Invoke(rabbitContext, next);
                }
                return Task.CompletedTask;
            };
            return next();
        }
    }
}
