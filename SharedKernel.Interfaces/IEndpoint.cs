using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace SharedKernel.Interfaces;

public interface IEndpoint
{
    void MapEndpoint(WebApplication app, IConfiguration configuration);
    
}