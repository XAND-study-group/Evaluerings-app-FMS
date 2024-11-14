using Microsoft.AspNetCore.Builder;

namespace SharedKernel.Interfaces;

public interface IEndpoint
{
    void MapEndpoint(WebApplication app);
}