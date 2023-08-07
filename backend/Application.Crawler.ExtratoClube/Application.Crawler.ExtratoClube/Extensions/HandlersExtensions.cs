using Application.Crawler.ExtratoClube.Commands.Users;
using Application.Crawler.ExtratoClube.DTOS;
using Application.Crawler.ExtratoClube.Handlers.Users;
using Application.Crawler.ExtratoClube.Interfaces.Commands;
using MediatR;

namespace Application.Crawler.ExtratoClube.Extensions;

public static class HandlersExtensions
{
    public static void AddCustomHandlers(this IServiceCollection services)
    {
        services.AddTransient<IRequestHandler<UserCommand, ICommandResult<UserResponse>>, UserCommandHandler>();
    }
}
