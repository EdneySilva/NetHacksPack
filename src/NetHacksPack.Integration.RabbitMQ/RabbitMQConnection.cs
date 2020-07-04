using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NetHacksPack.Integration.RabbitMQ
{
    internal delegate void OnConnection(IModel model, ConnectionOptions connectionOptions);
    class RabbitMQConnection : IDisposable
    {
        private readonly ConnectionOptions connectionOptions;
        private readonly IConnectionFactory connectionFactory;
        private readonly ILogger<RabbitMQConnection> logger;
        private CancellationToken cancellationToken;
        private IConnection currentConnection;
        private IModel currentChannel;
        private bool isConsumingAlive;
        private string queue;

        public event EventHandler OnReconnected;

        public ConnectionOptions Options => this.connectionOptions;

        internal RabbitMQConnection(ConnectionOptions connectionOptions, IConnectionFactory connectionFactory, ILogger<RabbitMQConnection> logger)
        {
            this.queue = connectionOptions.Queue;
            this.connectionOptions = connectionOptions;
            this.connectionFactory = connectionFactory;
            this.logger = logger;
        }

        public IConnection GetConnection()
        {
            if (this.currentConnection == null)
                this.currentConnection = connectionFactory.CreateConnection();
            return this.currentConnection;
        }

        public IModel Connect(CancellationToken cancellationToken, OnConnection onConnection)
        {
            this.cancellationToken = cancellationToken;
            this.currentConnection = this.GetConnection();
            this.currentChannel = this.currentConnection.CreateModel();
            onConnection?.Invoke(this.currentChannel, this.connectionOptions);
            this.isConsumingAlive = true;
            return this.currentChannel;
        }

        public int GetTimeout(int currentTimeOut = 1000)
        {
            const int limit = 30000;
            if (currentTimeOut < limit)
            {
                currentTimeOut = currentTimeOut * 2;
                return currentTimeOut;
            }
            return limit;
        }
        private void SafeDelay(int reconnectionTimeout, CancellationToken cancellationToken)
        {
            try
            {
                Task.Delay(reconnectionTimeout, cancellationToken).Wait();
            }
            catch (AggregateException ex)
            {
                foreach (var item in ex.InnerExceptions)
                {
                    if (item is OperationCanceledException)
                        throw item;
                }
            }
        }

        public void Reconnect()
        {
            var cancellationTaskSource = new CancellationTokenSource();
            isConsumingAlive = false;
            Task.Run(() =>
            {
                var eventReset = new ManualResetEventSlim(true);
                int attempts = 1;
                var reconnectionTimeout = 0;// this.GetTimeout();
                while (eventReset.Wait(reconnectionTimeout, this.cancellationToken) && !this.cancellationToken.IsCancellationRequested)
                {
                    var currentEvent = this.OnReconnected;

                    try
                    {
                        SafeDelay(reconnectionTimeout, cancellationTaskSource.Token);
                        reconnectionTimeout = this.GetTimeout(reconnectionTimeout > 0 ? reconnectionTimeout : 1000);
                        cancellationTaskSource.Token.ThrowIfCancellationRequested();
                        if (this.isConsumingAlive || this.cancellationToken.IsCancellationRequested)
                        {
                            eventReset.Set();
                            return;
                        }
                        this.ResetConnection();
                        if (currentEvent != null)
                        {
                            this.isConsumingAlive = true;
                            this.logger.LogInformation("Connection recovered with EventBus");
                            currentEvent(this, new EventArgs());
                        }
                        eventReset.Set();
                    }
                    catch (OperationCanceledException ex)
                    {
                        logger.LogCritical(ex, $"Timeout to connect into Rabbit Event bus after {attempts} attempts, please check the connection with the server, the reconnection retry was stopped and the application is not listening the EventBus");
                        eventReset.Set();
                        return;
                    }
                    catch (Exception ex)
                    {
                        isConsumingAlive = false;
                        logger.LogInformation($"An error ocurred when Trying to connect to RabbitMQ, Attempts: {attempts}");
                        logger.LogError("Error to connect on EventBus: \n" + ex.Message);
                        attempts++;

                    }
                }
            }, cancellationTaskSource.Token);
            cancellationTaskSource.CancelAfter(connectionOptions.ConnectionTimeout);
        }

        public void ResetConnection()
        {
            try
            {
                currentChannel?.Close();
            }
            finally
            {
                currentChannel = null;
            }
            try
            {
                currentConnection?.Close();
            }
            finally
            {
                currentConnection = null;
            }
        }

        public void Dispose()
        {
            this.currentChannel?.Dispose();
            this.currentConnection?.Dispose();
            this.currentChannel = null;
            this.currentConnection = null;
        }
    }
}
