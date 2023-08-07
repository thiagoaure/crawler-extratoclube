using Application.Crawler.ExtratoClube.Services;
using RabbitMQ.Client;

namespace Application.Crawler.ExtratoClube.AppContext;

public class RabbitContext
{
    public readonly IConnection Connection;
    public readonly IModel Channel;
    public readonly UserServiceConsumer UserServiceConsumer;
    public readonly string QUEUE_NAME = "cpfQueue";

    public RabbitContext()
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            Port = 5672,
        };
        Connection = factory.CreateConnection();
        Channel = Connection.CreateModel();
        Channel.QueueDeclare(QUEUE_NAME, true, false, false);

    }

    public void CloseConection()
    {
        Channel.Close();
        Connection.Close();
    }
}
