using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetHacksPack.Integration.RabbitMQ.Handler
{
    public delegate void RabbitPipelineConfigurator<T>(PipelineDescriptor pipelineDescriptor);
    public sealed class RabbitPipeline
    {
        private readonly Dictionary<string, PipelineDescriptor> handlers = new Dictionary<string, PipelineDescriptor>();

        public RabbitPipeline(Action<RabbitPipeline> configurePipeline)
        {
            configurePipeline(this);
        }

        public RabbitPipeline ConfigureEvent<T>(RabbitPipelineConfigurator<T> configurator)
        {
            var descriptor = new PipelineDescriptor(typeof(T).Name);
            configurator(descriptor);
            this.handlers.Add(typeof(T).Name, descriptor);
            return this;
        }

        internal PipelineDescriptor Describe<T>()
        {
            return this.Describe(typeof(T).Name);
        }

        internal PipelineDescriptor Describe(string eventName)
        {
            if (!this.handlers.ContainsKey(eventName))
                this.handlers.Add(eventName, new PipelineDescriptor(eventName));
            return this.handlers[eventName];
        }
    }

    public enum HandlerType : byte
    {
        Receive,
        Publish
    }

    public sealed class PipelineDescriptor
    {

        private readonly ExecutionPipe[] compiledPipe = new ExecutionPipe[2];
        public string EventName { get; }
        private readonly List<IRabbitMessageHandler> rabbitMessageHandlers = new List<IRabbitMessageHandler>();
        internal PipelineDescriptor(string eventName)
        {
            this.EventName = eventName;
        }

        internal PipelineDescriptor UseFirst(IRabbitMessageHandler handler)
        {
            this.rabbitMessageHandlers.Insert(0, handler);
            return this;
        }

        public PipelineDescriptor Use(IRabbitMessageHandler handler)
        {
            this.rabbitMessageHandlers.Add(handler);
            return this;
        }

        internal ExecutionPipe Build(HandlerType type)
        {
            return compiledPipe[type== HandlerType.Receive ? 0 : 1] = compiledPipe[type == HandlerType.Receive ? 0 : 1] ?? new ExecutionPipe(
                type == HandlerType.Receive ?
                rabbitMessageHandlers.Select(s => new RabbitMessageHandlerMethod(s.HandleReceive)).ToArray() :
                rabbitMessageHandlers.Select(s => new RabbitMessageHandlerMethod(s.HandlePublish)).ToArray()
            );
        }
    }
}
