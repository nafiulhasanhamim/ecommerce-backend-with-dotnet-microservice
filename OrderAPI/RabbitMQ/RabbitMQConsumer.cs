using System.Text;
using System.Text.Json;
using OrderAPI.DTOs;
using OrderAPI.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ProductAPI.Messaging
{
    public class RabbitMQConsumer : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMQConsumer(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;

            var factory = new ConnectionFactory
            {
                HostName = configuration.GetSection("RabbitMQ")["HostName"],
                UserName = configuration.GetSection("RabbitMQ")["UserName"],
                Password = configuration.GetSection("RabbitMQ")["Password"]
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            return Task.CompletedTask;
        }

        private void CreateConsumer(string queueName, Func<string, Task> processMessage)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"Received event from queue {queueName}: {message}");
                await processMessage(message);

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
