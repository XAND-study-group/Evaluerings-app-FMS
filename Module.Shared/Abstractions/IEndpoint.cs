using Microsoft.AspNetCore.Builder;
namespace Module.Shared.Abstractions;

public interface IEndpoint
{
    void MapEndpoint(WebApplication app);
}