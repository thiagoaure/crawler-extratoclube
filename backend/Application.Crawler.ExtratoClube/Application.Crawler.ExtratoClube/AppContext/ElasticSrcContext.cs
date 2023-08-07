using Nest;

namespace Application.Crawler.ExtratoClube.AppContext;

public class ElasticSrcContext
{
    public IElasticClient ElasticClient;

    public ElasticSrcContext()
    {
        ElasticClient = new ElasticClient(new ConnectionSettings(new Uri("http://localhost:9200"))
        .DefaultIndex("users"));
    }
}
