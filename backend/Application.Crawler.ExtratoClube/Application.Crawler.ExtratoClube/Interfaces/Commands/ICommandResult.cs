namespace Application.Crawler.ExtratoClube.Interfaces.Commands;

public interface ICommandResult<TData>
{
    bool Success { get; set; }
    string Message { get; set; }
    TData Data { get; set; }
}
