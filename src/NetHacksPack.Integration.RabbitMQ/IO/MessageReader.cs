using NetHacksPack.Integration.Abstractions.Exceptions;
using NetHacksPack.Integration.Abstractions.IO;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace NetHacksPack.Integration.RabbitMQ.IO
{

    class MessageReader : IMessageReader
    {
        private readonly BasicDeliverEventArgs basicDeliverEventArgs;
        private readonly ILogger<MessageReader> logger;
        private readonly MessageHeaders messageHeaders;
        private readonly byte[] body;
        private readonly IBasicProperties basicProperties;
        private readonly Encoding encoding;

        public MessageReader(byte[] body, IBasicProperties basicProperties, Encoding encoding)
        {
            this.body = body;
            this.basicProperties = basicProperties;
            this.encoding = encoding;
        }

        public MessageReader(BasicDeliverEventArgs basicDeliverEventArgs, ILogger<MessageReader> logger)
        {
            this.basicDeliverEventArgs = basicDeliverEventArgs;
            this.logger = logger;
            this.messageHeaders = new MessageHeaders(this.GetHeader, basicDeliverEventArgs);
        }

        private string GetHeader(string header, BasicDeliverEventArgs message)
        {
            if ((message.BasicProperties?.Headers?.ContainsKey(header) ?? false) == false)
                return string.Empty;
            return message.BasicProperties.Headers[header] is byte[]?
                Encoding.UTF8.GetString((byte[])message.BasicProperties.Headers[header]) :
                Convert.ToString(message.BasicProperties.Headers[header]);
        }

        public TResult Read<TResult>()
        {
            if (this.basicDeliverEventArgs.Body.IsEmpty)
                throw new MessageNullException();
            var messageString = this.messageHeaders.Encoding.GetString(this.basicDeliverEventArgs.Body.ToArray());
            if (string.IsNullOrEmpty(messageString?.Trim()))
                throw new MessageNullException();
            try
            {
                return JsonConvert.DeserializeObject<TResult>(messageString);
            }
            catch (Exception ex)
            {
                throw new MessageDeserializationException(ex);
            }   
        }

        public string ReadHeader(string headerName)
        {
            return null;
        }
    }
}