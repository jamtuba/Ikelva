using System;
using System.Text;
using System.Text.Json;
using Ikelva.ClassLibrary.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Ikelva.ClassLibrary.Helpers
{
    public class Consumer : IConsumer
    {
        private readonly IRedistributeMessages _redistributeMessages;

        public Consumer(IRedistributeMessages redistributeMessages)
        {
            _redistributeMessages = redistributeMessages;
        }
        public void ConsumeFromQueue(string queueName, string exchangeName, string routingKey)
        {
            var connection = GetConnection.ConnectionGetter();
            var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, true);

            channel.QueueDeclare(queueName, true, false, false);

            channel.QueueBind(queueName, exchangeName, routingKey);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, eventArgs) =>
            {
                var bytes = eventArgs.Body.ToArray();

                var body = Encoding.UTF8.GetString(bytes);

                var customer = JsonSerializer.Deserialize<Customer>(body);

                Console.WriteLine($"Besked modtaget fra {queueName}");

                var correlationId = eventArgs.BasicProperties.CorrelationId;

                if (!string.IsNullOrEmpty(customer.CustomerId))
                {
                    ShowData.DisplayData(customer, correlationId);
                    _redistributeMessages.SortMessages(customer, correlationId);
                }
                else
                {
                    var bankResponse = JsonSerializer.Deserialize<BankResponse>(body);
                    if (bankResponse.Response)
                    {
                        ShowData.DisplayData(bankResponse, correlationId);
                    }
                    else
                    {
                        ShowData.RejectedFunds();
                    }
                    _redistributeMessages.MessagesFromBank(bankResponse, correlationId);
                }
            };

            channel.BasicConsume(queueName, true, consumer);
        }
    }
}
