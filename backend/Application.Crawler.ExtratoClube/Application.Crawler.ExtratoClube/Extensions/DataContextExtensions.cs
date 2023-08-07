using Application.Crawler.ExtratoClube.AppContext;

namespace Application.Crawler.ExtratoClube.Extensions;

public static class DataContextExtensions
{
    public static void AddCustomDataContext(this IServiceCollection services)
    {
        services.AddScoped<RabbitContext>();
        services.AddScoped<RedisContext>();
        services.AddScoped<ElasticSrcContext>();
    }
}
