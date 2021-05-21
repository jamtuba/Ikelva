using Ikelva.ClassLibrary.Models;

namespace Ikelva.ClassLibrary.Helpers
{
    public interface IProducer
    {
        public void CreateProducer(Customer customer, string queue = "", string routingKey = "");
        public void CreateProducer(BankCustomer customer, string queue = "", string routingKey = "", string correlationId = "");
        void CreateProducer(BankResponse bankResponse, string queue, string routingKey, string correlationId);
    }
}
