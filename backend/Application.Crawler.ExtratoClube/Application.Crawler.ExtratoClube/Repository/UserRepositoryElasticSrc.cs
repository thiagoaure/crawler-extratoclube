using Application.Crawler.ExtratoClube.AppContext;
using Application.Crawler.ExtratoClube.Commands.Helpers;
using Application.Crawler.ExtratoClube.DTOS;
using Application.Crawler.ExtratoClube.Entities;
using Application.Crawler.ExtratoClube.Interfaces.Commands;
using Application.Crawler.ExtratoClube.Interfaces.Repository;
using Nest;

namespace Application.Crawler.ExtratoClube.Repository;

public class UserRepositoryElasticSrc : IUserRepositoryElasticSrc
{
    private readonly ElasticSrcContext _elasticSrcContext;

    public UserRepositoryElasticSrc(ElasticSrcContext elasticSrcContext)
    {
        _elasticSrcContext = elasticSrcContext;
    }

    public async Task<ICommandResult<UserResponse>> GetUserByCpf(string cpf)
    {
        var searched = await _elasticSrcContext.ElasticClient.SearchAsync<User>(s => s
            .Query(q => q
                .Match(m => m
                    .Field(f => f.Cpf)
                    .Query(cpf)
                )
            )
        );
        if (!searched.IsValid)
        {
            throw new Exception($"erro to get user: {searched.DebugInformation}");
        }

        var src = searched.Hits.FirstOrDefault()?.Source;
        if (src != null)
        {
            var response = new UserResponse
            {
                RegistrationNumber = src?.RegistrationNumber,
                ExistRegistrationNumber = true
            };
            return new CommandResult<UserResponse>(true, "", response);
        }
        return new CommandResult<UserResponse>(true, "Nenhum beneficio para esse cpf no sistema", new UserResponse());
    }

    public async Task<ICommandResult<UserResponse>> SaveUser(User user)
    {
        var response = await _elasticSrcContext.ElasticClient.IndexDocumentAsync(user);
        if (!response.IsValid)
        {
            throw new Exception($"erro to index user: {response.DebugInformation}");
        }

        var res = new UserResponse
        {
            RegistrationNumber = user.RegistrationNumber,
            ExistRegistrationNumber = true
        };

        return new CommandResult<UserResponse>(true, "", res);
    }
}
