using Application.Crawler.ExtratoClube.Interfaces.Repository;
using Application.Crawler.ExtratoClube.Repository;

namespace Application.Crawler.ExtratoClube.Extensions;

public static class RepositoryExtensions
{
    public static void AddCustomRepository(this IServiceCollection services)
    {
        services.AddTransient<IUserRepositoryRedis, UserRepositoryRedis>();
        services.AddTransient<IUserRepositoryElasticSrc, UserRepositoryElasticSrc>();
    }
}
