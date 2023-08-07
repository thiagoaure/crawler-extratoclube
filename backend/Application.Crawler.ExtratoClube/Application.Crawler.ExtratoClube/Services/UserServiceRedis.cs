using Application.Crawler.ExtratoClube.DTOS;
using Application.Crawler.ExtratoClube.Entities;
using Application.Crawler.ExtratoClube.Interfaces.Commands;
using Application.Crawler.ExtratoClube.Interfaces.Repository;
using Application.Crawler.ExtratoClube.Interfaces.Services;

namespace Application.Crawler.ExtratoClube.Services;

public class UserServiceRedis : IUserServiceRedis
{
    private readonly IUserRepositoryRedis _userRepositoryRedis;

    public UserServiceRedis(IUserRepositoryRedis userRepositoryRedis)
    {
        _userRepositoryRedis = userRepositoryRedis;
    }

    public async Task<ICommandResult<UserResponse>> GetRegistrationNumber(User user)
    {
        return await _userRepositoryRedis.GetRegistrationNumber(user);
    }

    public async Task<ICommandResult<UserResponse>> SaveUser(User user)
    {
        return await _userRepositoryRedis.SaveUser(user);
    }
}
