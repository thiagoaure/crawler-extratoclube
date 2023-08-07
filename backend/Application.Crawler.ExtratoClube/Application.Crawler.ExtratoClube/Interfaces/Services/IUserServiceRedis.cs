using Application.Crawler.ExtratoClube.DTOS;
using Application.Crawler.ExtratoClube.Entities;
using Application.Crawler.ExtratoClube.Interfaces.Commands;

namespace Application.Crawler.ExtratoClube.Interfaces.Services;

public interface IUserServiceRedis
{
    Task<ICommandResult<UserResponse>> GetRegistrationNumber(User user);
    Task<ICommandResult<UserResponse>> SaveUser(User user);
}
