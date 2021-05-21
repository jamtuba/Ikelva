using System;
using RabbitMQ.Client;

namespace Ikelva.ClassLibrary
{
    public static class GetConnection
    {
        public static IConnection ConnectionGetter()
        {
            var hostName = "Janus-LAPTOP";
            var vhost = "janus_vhost";
            var userName = "janus";
            var password = "123";
            var factory = new ConnectionFactory();

            factory.Uri = new Uri($"amqp://{userName}:{password}@{hostName}/{vhost}");

            var connection = factory.CreateConnection();

            return connection;
        }
    }
}