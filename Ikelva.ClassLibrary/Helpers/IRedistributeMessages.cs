using Ikelva.ClassLibrary.Models;

namespace Ikelva.ClassLibrary.Helpers
{
    public interface IRedistributeMessages
    {
        void SortMessages(Customer customer, string eventArgs);
        void MessagesFromBank(BankResponse bankResponse, string correlationId);
    }
}