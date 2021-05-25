using System;
using Ikelva.ClassLibrary.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Ikelva.RoutingApp
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("Routing App er begyndt at lytte!");
            Console.WriteLine("------------------------------");

            var host = AppStartup();
            
            var consumer1 = ActivatorUtilities.CreateInstance<Consumer>(host.Services);
            var consumer2 = ActivatorUtilities.CreateInstance<Consumer>(host.Services);

            consumer1.ConsumeFromQueue(EndPoints.WebsiteQueueName, EndPoints.ExchangeName, EndPoints.WebsiteRoutingKey);

            consumer2.ConsumeFromQueue(EndPoints.FromBankQueueName, EndPoints.ExchangeName, EndPoints.FromBankRoutingKey);

            Console.ReadLine();
        }

        private static IHost AppStartup()
        {
            var host = Host.CreateDefaultBuilder()
                       .ConfigureServices((context, services) => {
                           services.AddSingleton<IConsumer, Consumer>();
                           services.AddScoped<IRedistributeMessages, RedistributeMessages>();
                           services.AddScoped<IProducer, Producer>();
                       })
                       .Build();

            return host;
        }
    }
}
