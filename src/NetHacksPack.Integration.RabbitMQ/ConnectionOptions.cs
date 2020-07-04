using NetHacksPack.Core;

namespace NetHacksPack.Integration.RabbitMQ
{
    public class ConnectionOptions : IReplaceable<ConnectionOptions>
    {
        public string BrokerName { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string VirtualHost { get; set; }
        public string Queue { get; set; }
        public ushort? HearthBeat { get; set; }
        public int ConnectionTimeout { get; set; } = 70000;

        public ConnectionOptions CopyAndReplace(ConnectionOptions destination)
        {
            destination.BrokerName = destination.BrokerName;
            destination.Host = string.IsNullOrEmpty(destination.Host) ? this.Host : destination.Host;
            destination.Port = destination.Port == 0 ? this.Port : destination.Port;
            destination.UserName = string.IsNullOrEmpty(destination.UserName) ? this.UserName : destination.UserName;
            destination.Password = string.IsNullOrEmpty(destination.Password) ? this.Password : destination.Password;
            destination.VirtualHost = string.IsNullOrEmpty(destination.VirtualHost) ? this.VirtualHost : destination.VirtualHost;
            destination.Queue = string.IsNullOrEmpty(destination.Queue) ? this.Queue : destination.Queue;
            destination.HearthBeat = !destination.HearthBeat.HasValue ? this.HearthBeat : destination.HearthBeat;
            destination.ConnectionTimeout = destination.ConnectionTimeout == 70000 ? this.ConnectionTimeout : destination.ConnectionTimeout;
            return destination;
        }
    }
}
