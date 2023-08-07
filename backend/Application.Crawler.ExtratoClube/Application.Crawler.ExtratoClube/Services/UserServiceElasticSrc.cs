using Application.Crawler.ExtratoClube.DTOS;
using Application.Crawler.ExtratoClube.Entities;
using Application.Crawler.ExtratoClube.Interfaces.Commands;
using Application.Crawler.ExtratoClube.Interfaces.Repository;
using Application.Crawler.ExtratoClube.Interfaces.Services;

namespace Application.Crawler.ExtratoClube.Services;

public class UserServiceElasticSrc : IUserElasticSrcService
{
    private readonly IUserRepositoryElasticSrc _userRepositoryElasticSrc;

    public UserServiceElasticSrc(IUserRepositoryElasticSrc userRepositoryElasticSrc)
    {
        _userRepositoryElasticSrc = userRepositoryElasticSrc;
    }

    public async Task<ICommandResult<UserResponse>> GetUserByCpf(string cpf)
    {
        return await _userRepositoryElasticSrc.GetUserByCpf(cpf);
    }

    public async Task<ICommandResult<UserResponse>> SaveUser(User user)
    {
        return await _userRepositoryElasticSrc.SaveUser(user);
    }
}
