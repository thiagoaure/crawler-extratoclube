using Application.Crawler.ExtratoClube.Commands.Helpers;
using Application.Crawler.ExtratoClube.Commands.Users;
using Application.Crawler.ExtratoClube.DTOS;
using Application.Crawler.ExtratoClube.Entities;
using Application.Crawler.ExtratoClube.Interfaces.Commands;
using Application.Crawler.ExtratoClube.Interfaces.Services;
using MediatR;

namespace Application.Crawler.ExtratoClube.Handlers.Users;

public class UserCommandHandler : IRequestHandler<UserCommand, ICommandResult<UserResponse>>
{
    private readonly IUserServiceRedis _userServiceRedis;
    private readonly IUserExtratoClubeService _userExtratoClubeService;
    private readonly IUserElasticSrcService _userElasticSrcService;

    public UserCommandHandler(IUserServiceRedis userServiceRedis, 
        IUserExtratoClubeService userExtratoClubeService,
        IUserElasticSrcService userElasticSrcService)
    {
        _userServiceRedis = userServiceRedis;
        _userExtratoClubeService = userExtratoClubeService;
        _userElasticSrcService = userElasticSrcService;
    }

    public async Task<ICommandResult<UserResponse>> Handle(UserCommand request, CancellationToken cancellationToken)
    {
        var user = new User { 
            Cpf = request.Cpf,
            Login = request.Login,
            Password = request.Password,
        };
        var response = await _userServiceRedis.GetRegistrationNumber(user);
        if (!response.Data.ExistRegistrationNumber) 
        {
            response = await _userExtratoClubeService.GetRegistrationNumber(user);
            if (response.Data.ExistRegistrationNumber)
            {
                user.RegistrationNumber = response.Data.RegistrationNumber;
                await _userServiceRedis.SaveUser(user);
                await _userElasticSrcService.SaveUser(user);
                return new CommandResult<UserResponse>(true, "cpf com beneficio processado e salvo na base de dados", response.Data);
            }
        }
        return new CommandResult<UserResponse>(true, $"Sem beneficios para esse cpf", response.Data); ;
    }
    
}
