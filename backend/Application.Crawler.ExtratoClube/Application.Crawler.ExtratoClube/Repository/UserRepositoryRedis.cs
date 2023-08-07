using Application.Crawler.ExtratoClube.AppContext;
using Application.Crawler.ExtratoClube.Commands.Helpers;
using Application.Crawler.ExtratoClube.DTOS;
using Application.Crawler.ExtratoClube.Entities;
using Application.Crawler.ExtratoClube.Interfaces.Commands;
using Application.Crawler.ExtratoClube.Interfaces.Repository;
using Newtonsoft.Json;

namespace Application.Crawler.ExtratoClube.Repository;

public class UserRepositoryRedis : IUserRepositoryRedis
{
    private readonly RedisContext _redisContext;

    public UserRepositoryRedis(RedisContext redisContext)
    {
        _redisContext = redisContext;
    }

    public async Task<ICommandResult<UserResponse>> GetRegistrationNumber(User user)
    {
        var keys = _redisContext.redis.GetServer("localhost:6379").Keys();

        foreach (var key in keys)
        {
            string jsonValue = await _redisContext.database.StringGetAsync(key);
            User jsonUser = JsonConvert.DeserializeObject<User>(jsonValue);
            if (jsonUser.Cpf == user.Cpf)
            {
                var searched = new UserResponse
                {
                    RegistrationNumber = jsonUser.RegistrationNumber,
                    ExistRegistrationNumber = true
                };
                return new CommandResult<UserResponse>(true, "", searched);
            }
        }
        return new CommandResult<UserResponse>(false, "", new UserResponse());
    }

    public async Task<ICommandResult<UserResponse>> SaveUser(User user)
    {
        var serializedUser = JsonConvert.SerializeObject(user);

        var response = await _redisContext.database.StringSetAsync(user.Cpf, serializedUser);
        if (response)
        {
            return new CommandResult<UserResponse>(true, "", new UserResponse());
        }
        return null;
    }
}
