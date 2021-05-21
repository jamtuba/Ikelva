using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Ikelva.ClassLibrary;
using Ikelva.ClassLibrary.Helpers;
using Ikelva.ClassLibrary.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Ikelva.BankApp
{
    static class BankApp
    {
        private static readonly string exchangeName = EndPoints.ExchangeName;

        static void Main(string[] args)
        {
            Console.WriteLine("BankApp begynder at lytte med:");
            Console.WriteLine("------------------------------");
            ConsumeFromQueue(EndPoints.ToBankQueueName, EndPoints.ToBankRoutingKey);
            Console.ReadLine();
        }

        public static void ConsumeFromQueue(string queueName, string routingKey)
        {
            var connection = GetConnection.ConnectionGetter();
            var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, true);

            channel.QueueDeclare(queueName, true, false, false);

            channel.QueueBind(queueName, exchangeName, routingKey);

            var consumer = new EventingBasicConsumer(channel);

            List<string> corIds = new List<string>();

            consumer.Received += (sender, eventArgs) =>
            {
                var bytes = eventArgs.Body.ToArray();

                var body = Encoding.UTF8.GetString(bytes);

                var customer = JsonSerializer.Deserialize<BankCustomer>(body);

                Console.WriteLine("Besked modtaget:");
                Console.WriteLine("- - - - - - - - - - - - - - -");
                Console.WriteLine($"Navn: {customer.FullName}");
                Console.WriteLine($"Email: {customer.EmailAddress}");
                Console.WriteLine("------------------------------");

                var corId = eventArgs.BasicProperties.CorrelationId;

                if (!corIds.Contains(corId))
                {
                    corIds.Add(corId);
                    SendResponse(customer, eventArgs.BasicProperties.CorrelationId);
                }
            };

            var result = channel.BasicConsume(queueName, true, consumer);
        }

        private static void SendResponse(BankCustomer customer, string correlationId)
        {
            var bankResponse = new BankResponse()
            {
                FullName = customer.FullName,
                EmailAddress = customer.EmailAddress,
                Response = true
            };

            var queue = EndPoints.FromBankQueueName;
            var routingKey = EndPoints.FromBankRoutingKey;

            var connection = GetConnection.ConnectionGetter();
            using var channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange: exchangeName,
                                    durable: true,
                                    type: ExchangeType.Direct);

            channel.QueueDeclare(queue: queue,
                                    durable: true,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

            channel.QueueBind(queue, exchangeName, routingKey);

            Console.WriteLine("Besked sendt:");
            Console.WriteLine("- - - - - - - - - - - - - - -");
            Console.WriteLine($"Navn: {bankResponse.FullName}");
            Console.WriteLine($"Email: {bankResponse.EmailAddress}");
            Console.WriteLine($"CorrelationId: {correlationId}");
            Console.WriteLine($"Svar fra banken: {(bankResponse.Response ? "Positivt" : "Negativt")}");
            Console.WriteLine("------------------------------");

            var message = JsonSerializer.Serialize(bankResponse);

            var body = Encoding.UTF8.GetBytes(message);

            var props = channel.CreateBasicProperties();
            props.CorrelationId = correlationId;
            props.Persistent = true;

            channel.BasicPublish(exchange: exchangeName,
                                    routingKey: routingKey,
                                    basicProperties: props,
                                    body: body);

            CloseConnection.CloseAll(channel, connection);
        }
    }
}
