using System;
using System.Text;
using System.Text.Json;
using Ikelva.ClassLibrary.Models;
using RabbitMQ.Client;

namespace Ikelva.ClassLibrary.Helpers
{
    public class Producer : IProducer
    {
        static string exchange = EndPoints.ExchangeName;

        public void CreateProducer(Customer customer, string queue, string routingKey)
        {
            queue = EndPoints.WebsiteQueueName;
            routingKey = EndPoints.WebsiteRoutingKey;

            var connection = GetConnection.ConnectionGetter();
            using var channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange: exchange,
                                    durable: true,
                                    type: ExchangeType.Direct);

            channel.QueueDeclare(queue: queue,
                                    durable: true,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

            channel.QueueBind(queue, exchange, routingKey);

            var message = JsonSerializer.Serialize(customer);

            var body = Encoding.UTF8.GetBytes(message);

            var props = channel.CreateBasicProperties();
            props.CorrelationId = customer.CustomerId;
            props.Persistent = true;

            Console.WriteLine($"Sender til {queue}");
            ShowData.DisplayData(customer, null);

            channel.BasicPublish(exchange: exchange,
                                    routingKey: routingKey,
                                    basicProperties: props,
                                    body: body);

            CloseConnection.CloseAll(channel, connection);
        }

        public void CreateProducer(BankCustomer bankCustomer, string queue, string routingKey, string correlationId)
        {
            var connection = GetConnection.ConnectionGetter();
            using var channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange: exchange,
                                    durable: true,
                                    type: ExchangeType.Direct);

            channel.QueueDeclare(queue: queue,
                                    durable: true,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

            channel.QueueBind(queue, exchange, routingKey);

            var message = JsonSerializer.Serialize(bankCustomer);

            var body = Encoding.UTF8.GetBytes(message);

            var props = channel.CreateBasicProperties();
            props.CorrelationId = correlationId;
            props.Persistent = true;

            Console.WriteLine($"Sender til {queue}");
            ShowData.DisplayData(bankCustomer, correlationId);

            channel.BasicPublish(exchange: exchange,
                                    routingKey: routingKey,
                                    basicProperties: props,
                                    body: body);

            CloseConnection.CloseAll(channel, connection);
        }

        public void CreateProducer(BankResponse bankResponse, string queue, string routingKey, string correlationId)
        {
            var connection = GetConnection.ConnectionGetter();
            using var channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange: exchange,
                                    durable: true,
                                    type: ExchangeType.Direct);

            channel.QueueDeclare(queue: queue,
                                    durable: true,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

            channel.QueueBind(queue, exchange, routingKey);

            var message = JsonSerializer.Serialize(bankResponse);

            var body = Encoding.UTF8.GetBytes(message);

            var props = channel.CreateBasicProperties();
            props.CorrelationId = correlationId;

            Console.WriteLine($"Sender til {queue}");
            ShowData.DisplayData(bankResponse, correlationId);

            channel.BasicPublish(exchange: exchange,
                                    routingKey: routingKey,
                                    basicProperties: props,
                                    body: body);

            CloseConnection.CloseAll(channel, connection);
        }
    }
}
