using Application.Crawler.ExtratoClube.Interfaces.Services;
using Application.Crawler.ExtratoClube.Services;
using Application.Crawler.ExtratoClube.Services.Crawler;
using Application.Crawler.ExtratoClube.Services.Crawler.Models;

namespace Application.Crawler.ExtratoClube.Extensions;

public static class ServicesExtensions
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddTransient<IUserServiceRedis, UserServiceRedis>();
        services.AddTransient<IUserExtratoClubeService, UserExtratoClubeService>();
        services.AddTransient<IUserElasticSrcService, UserServiceElasticSrc>();
        services.AddTransient<IUserServiceConsumer, UserServiceConsumer>();
        services.AddTransient<IExtratoClubeService, ExtratoClubeService>();
    }
}
