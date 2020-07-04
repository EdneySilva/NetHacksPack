using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace NetHacksPack.Integration.RabbitMQ.IO
{
    struct MessageHeaders
    {
        public MessageHeaders(Func<string, BasicDeliverEventArgs, string> headerGetter, BasicDeliverEventArgs basicDeliverEventArgs)
        {
            int.TryParse(headerGetter(Constants.Headers.TTL, basicDeliverEventArgs), out int ttl);
            bool.TryParse(headerGetter(Constants.Headers.COMPRESSED, basicDeliverEventArgs), out bool compressed);
            Guid.TryParse(headerGetter(Constants.Headers.FILEID, basicDeliverEventArgs), out Guid fileId);
            int.TryParse(headerGetter(Constants.Headers.CURRENT_CHUNCK, basicDeliverEventArgs), out int currentChunck);
            int.TryParse(headerGetter(Constants.Headers.TOTAL_CHUNCK, basicDeliverEventArgs), out int totalChunck);
            string encoding = headerGetter(Constants.Headers.ENCODING, basicDeliverEventArgs);
            this.Ttl = ttl;
            this.IsCompressed = compressed;
            this.FileId = fileId;
            this.CurrentChunck = currentChunck;
            this.TotalChunck = totalChunck;
            if (string.IsNullOrEmpty(encoding))
                this.Encoding = Encoding.UTF8;
            else
                this.Encoding = Encoding.GetEncoding(encoding);
        }
        public int Ttl { get; set; }
        public bool IsCompressed { get; set; }
        public Guid FileId { get; set; }
        public int CurrentChunck { get; set; }
        public int TotalChunck { get; set; }
        public Encoding Encoding { get; set; }
    }
}