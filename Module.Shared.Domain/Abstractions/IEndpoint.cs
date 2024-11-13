using Microsoft.AspNetCore.Builder;

namespace Module.Shared.Domain.Abstractions;

public interface IEndpoint
{
    void MapEndpoint(WebApplication app);
}