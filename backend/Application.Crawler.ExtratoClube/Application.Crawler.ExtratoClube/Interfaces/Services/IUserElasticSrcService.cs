using Application.Crawler.ExtratoClube.DTOS;
using Application.Crawler.ExtratoClube.Entities;
using Application.Crawler.ExtratoClube.Interfaces.Commands;

namespace Application.Crawler.ExtratoClube.Interfaces.Services;

public interface IUserElasticSrcService
{
    Task<ICommandResult<UserResponse>> GetUserByCpf(string cpf);
    Task<ICommandResult<UserResponse>> SaveUser(User user);
}
