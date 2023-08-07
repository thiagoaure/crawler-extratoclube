using Application.Crawler.ExtratoClube.DTOS;
using Application.Crawler.ExtratoClube.Entities;
using Application.Crawler.ExtratoClube.Interfaces.Commands;

namespace Application.Crawler.ExtratoClube.Interfaces.Services;

public interface IExtratoClubeService
{
    bool SignIn(string username, string password);
    ICommandResult<UserResponse> ExtractBenefit(User user);
}