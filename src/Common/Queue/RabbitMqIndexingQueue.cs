using System;
using System.Net;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Common.Queue
{
    /// <summary>
    /// Rabbit Mq implementation of IIndexingQueue. Using RabbitMQ as queue service
    /// </summary>
    public class RabbitMqIndexingQueue : IIndexingQueue
    {
        private readonly string _hostname;
        private readonly string _password;
        private readonly string _username;
        private readonly string _queueName;
        private IConnection _connection;
        private IModel _channel;

        /// <summary>
        /// Creates new RabbitMq queue
        /// </summary>
        /// <param name="rabbitMqOptions">RabbitMq config</param>
        /// <param name="queueName">Default queue name</param>
        public RabbitMqIndexingQueue(IOptions<RabbitMqConfiguration> rabbitMqOptions, string queueName)
        {
            _hostname = rabbitMqOptions.Value.Hostname;
            _username = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;
            _queueName = queueName;

            CreateConnection();
        }

        /// <summary>
        /// Adds new url to queue
        /// </summary>
        /// <param name="url">Url to be added</param>
        public void Add(string url)
        {
            if (ConnectionExists())
            {
                using (var channel = _connection.CreateModel())
                {
                    channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                    channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: Encoding.UTF8.GetBytes(url));
                }
            }
        }

        /// <summary>
        /// Listens to new Urls added to queue
        /// </summary>
        /// <param name="onIndexAdded">Action, that will be called when new item is added</param>
        public void StartListening(Action<string> onIndexAdded)
        {
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (ch, ea) =>
            {
                var url = Encoding.UTF8.GetString(ea.Body.ToArray());
                onIndexAdded.Invoke(url);
                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(_queueName, false, consumer);
        }

        /// <summary>
        /// Creates connection to Rabbit MQ
        /// </summary>
        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostname,
                    UserName = _username,
                    Password = _password
                };
                _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not create connection: {ex.Message}");
            }
        }

        /// <summary>
        /// Checks, whether a connection was created. If not, creates new connection.
        /// </summary>
        /// <returns>True when connection exists or when connection was successfully created</returns>
        private bool ConnectionExists()
        {
            if (_connection != null)
            {
                return true;
            }

            CreateConnection();

            return _connection != null;
        }
    }
}
