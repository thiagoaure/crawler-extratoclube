using Application.Crawler.ExtratoClube.DTOS;
using Application.Crawler.ExtratoClube.Entities;
using Application.Crawler.ExtratoClube.Interfaces.Commands;
using Application.Crawler.ExtratoClube.Interfaces.Services;
using Application.Crawler.ExtratoClube.Services.Crawler.Models;

namespace Application.Crawler.ExtratoClube.Services.Crawler;

public class UserExtratoClubeService : IUserExtratoClubeService
{
    private IExtratoClubeService _extratoClubeService;
    private ExtratoClubeService ExtratoClubeService = new ExtratoClubeService();

    public UserExtratoClubeService(IExtratoClubeService extratoClubeService)
    {
        _extratoClubeService = extratoClubeService;
    }

    public async Task<ICommandResult<UserResponse>> GetRegistrationNumber(User user)
    {
        return _extratoClubeService.ExtractBenefit(user);
    }
}
