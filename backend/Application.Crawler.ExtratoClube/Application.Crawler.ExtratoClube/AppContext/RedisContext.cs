using StackExchange.Redis;

namespace Application.Crawler.ExtratoClube.AppContext;

public class RedisContext
{
    public readonly IDatabase database;
    public readonly ConnectionMultiplexer redis;

    public RedisContext()
    {
        redis = ConnectionMultiplexer.Connect("localhost");
        database = redis.GetDatabase();
    }
}
