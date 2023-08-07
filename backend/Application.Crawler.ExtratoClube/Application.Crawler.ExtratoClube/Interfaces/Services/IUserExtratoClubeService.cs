using Application.Crawler.ExtratoClube.Commands.Users;
using Application.Crawler.ExtratoClube.DTOS;
using Application.Crawler.ExtratoClube.Entities;
using Application.Crawler.ExtratoClube.Interfaces.Commands;

namespace Application.Crawler.ExtratoClube.Interfaces.Services;

public interface IUserExtratoClubeService
{
    Task<ICommandResult<UserResponse>> GetRegistrationNumber(User user);
}
