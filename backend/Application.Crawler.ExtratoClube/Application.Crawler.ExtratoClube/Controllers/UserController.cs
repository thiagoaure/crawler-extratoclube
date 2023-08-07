using Application.Crawler.ExtratoClube.Commands.Users;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using Application.Crawler.ExtratoClube.AppContext;
using Application.Crawler.ExtratoClube.Interfaces.Services;
using Application.Crawler.ExtratoClube.Commands.Helpers;

namespace Application.Crawler.ExtratoClube.Controllers;

[Route("api/v1")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly RabbitContext _rabbitContext;
    private readonly IUserElasticSrcService _userElasticSrcService;

    public UserController(RabbitContext rabbitContext, IUserElasticSrcService userElasticSrcService)
    {
        _rabbitContext = rabbitContext;
        _userElasticSrcService = userElasticSrcService;
    }


    [HttpPost("user/queue")]
    public async Task<IActionResult> Post([FromBody] UserCommand userRequest)
    {
        SendMessageQueue(userRequest);
        var response = new CommandResult<UserCommand> { Success = true, Message ="Sua Solicitação foi enviada, acompanhe informando o cpf no endpoint de consulta", Data = userRequest };

        return new OkObjectResult(response);
    }

    [HttpGet("user/{cpf}")]
    public async Task<IActionResult> GetRegistrationNumberByCpf(string cpf)
    {
        var response = await  _userElasticSrcService.GetUserByCpf(cpf);
        return new OkObjectResult(response);
    }

    private void SendMessageQueue(UserCommand userRequest)
    {
        string mensagem = JsonSerializer.Serialize(userRequest);
        byte[] body = Encoding.UTF8.GetBytes(mensagem);

        try 
        { 

            _rabbitContext.Channel.BasicPublish("", _rabbitContext.QUEUE_NAME, null, body);

        } catch (Exception ex) 
        {

            throw new Exception($"Exception: {ex.GetType().FullName} | " +
                             $"Message: {ex.Message}");
        }
        finally 
        {
            _rabbitContext.CloseConection();
        }
    }

}
