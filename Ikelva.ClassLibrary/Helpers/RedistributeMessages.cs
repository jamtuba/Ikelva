using Ikelva.ClassLibrary.Models;
using RabbitMQ.Client.Events;

namespace Ikelva.ClassLibrary.Helpers
{
    public class RedistributeMessages : IRedistributeMessages
    {
        private readonly IProducer _producer;

        public RedistributeMessages(IProducer producer)
        {
            _producer = producer;
        }

        public void SortMessages(Customer customer, string correlationId)
        {
            var bankCust = new BankCustomer()
            {
                FullName = customer.FirstName + " " + customer.LastName,
                EmailAddress = customer.EmailAddress
            };

            _producer.CreateProducer(bankCust, EndPoints.ToBankQueueName, EndPoints.ToBankRoutingKey, correlationId);
            // Flere producers til andre endpoints
        }

        public void MessagesFromBank(BankResponse bankResponse, string correlationId)
        {
            if (bankResponse.Response)
            {
                _producer.CreateProducer(bankResponse, EndPoints.BankResponseQueueName, EndPoints.BankResponseRoutingKey, correlationId);
            }
            else
            {
                _producer.CreateProducer(bankResponse, EndPoints.InvalidQueueName, EndPoints.InvalidRoutingKey, correlationId);
            }
        }
    }
}
