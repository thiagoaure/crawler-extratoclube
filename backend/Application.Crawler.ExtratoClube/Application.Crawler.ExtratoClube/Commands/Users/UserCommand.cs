using Application.Crawler.ExtratoClube.DTOS;
using Application.Crawler.ExtratoClube.Interfaces.Commands;
using MediatR;

namespace Application.Crawler.ExtratoClube.Commands.Users;

public class UserCommand : IRequest<ICommandResult<UserResponse>>
{
    public string Cpf { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
