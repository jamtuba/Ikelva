using System;
using System.Collections.Generic;
using System.Text;

namespace Ikelva.ClassLibrary.Helpers
{
    public interface IConsumer
    {
        void ConsumeFromQueue(string queueName, string exchangeName, string routingKey = "");
    }
}
