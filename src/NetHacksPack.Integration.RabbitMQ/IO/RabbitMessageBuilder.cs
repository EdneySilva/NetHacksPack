using RabbitMQ.Client;
using System;
using System.Collections.Generic;

namespace NetHacksPack.Integration.RabbitMQ.IO
{
    public struct RabbitMessageBuilder
    {
        private IModel channel;
        private Func<IModel, IBasicProperties> propertyFactory;
        private List<Tuple<IBasicProperties, byte[]>> buffer;

        public RabbitMessageBuilder(IModel channel, Func<IModel, IBasicProperties> propertyFactory, List<Tuple<IBasicProperties, byte[]>> buffer)
        {
            this.channel = channel;
            this.propertyFactory = propertyFactory;
            this.buffer = buffer;
        }

        public void BuildMessage(byte[] message, IEnumerable<Action<IBasicProperties>> messageSpecification)
        {
            var property = propertyFactory(this.channel);
            foreach (var specification in messageSpecification)
                specification(property);
            var array = message;
            this.buffer.Add(new Tuple<IBasicProperties, byte[]>(property, message));
        }
    }
}
