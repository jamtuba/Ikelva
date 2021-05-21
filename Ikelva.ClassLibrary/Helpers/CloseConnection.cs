using RabbitMQ.Client;

namespace Ikelva.ClassLibrary.Helpers
{
    public static class CloseConnection
    {
        public static void CloseAll(IModel channel, IConnection connection)
        {
            channel.Close();
            connection.Close();
        }
    }
}
