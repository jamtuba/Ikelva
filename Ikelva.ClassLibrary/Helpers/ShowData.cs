using System;
using RabbitMQ.Client.Events;

namespace Ikelva.ClassLibrary.Helpers
{
    static class ShowData
    {
        public static void DisplayData(Object obj, string correlationId)
        {
            Console.WriteLine("- - - - - - - - - - - - - - -");
            foreach (var property in obj.GetType().GetProperties())
            {
                Console.WriteLine($"{property.Name}: {property.GetValue(obj)}");
            }

            if (!string.IsNullOrEmpty(correlationId))
            {
                Console.WriteLine($"CorrelationId: {correlationId}");
            }
            Console.WriteLine("------------------------------");
        }

        public static void RejectedFunds()
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Der var ikke dækning til købet!");
            Console.WriteLine("--------------------------------");
        }
    }
}
