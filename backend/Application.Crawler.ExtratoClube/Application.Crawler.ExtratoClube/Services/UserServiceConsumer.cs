using Application.Crawler.ExtratoClube.Commands.Users;
using Application.Crawler.ExtratoClube.Entities;
using Application.Crawler.ExtratoClube.Interfaces.Services;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Application.Crawler.ExtratoClube.Services;

public class UserServiceConsumer : IUserServiceConsumer
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private const string QUEUE_NAME = "cpfQueue";
    private readonly IMediator _mediator;

    public UserServiceConsumer(IMediator mediator)
    {
        _mediator = mediator;
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            Port = 5672,
        };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(QUEUE_NAME, true, false, false);

    }

    public void ProcessMessages()
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            try
            {
                string mensagem = Encoding.UTF8.GetString(ea.Body.ToArray());
                Console.WriteLine("Mensagem recebida: " + mensagem);
                User user = JsonConvert.DeserializeObject<User>(mensagem);
                _channel.BasicAck(ea.DeliveryTag, false);
                var post = new UserCommand
                {
                    Cpf = user.Cpf,
                    Login = user.Login,
                    Password = user.Password
                };
                var response = await _mediator.Send(post);
                Console.WriteLine($"{post.Cpf}:  {response.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro ao ler a mensagem: " + ex.Message);
                _channel.BasicReject(ea.DeliveryTag, true);
            }
        };

        _channel.BasicConsume(QUEUE_NAME, false, consumer);

    }
}
