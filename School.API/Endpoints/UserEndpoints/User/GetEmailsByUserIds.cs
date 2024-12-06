using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Application.Features.UserFeature.User.Query;
using SharedKernel.Dto.Features.School.User.Query;
using SharedKernel.Interfaces;

namespace School.API.Endpoints.UserEndpoints.User;

public class GetEmailsByUserIds : IEndpoint
{
    public void MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapPost(configuration["Routes:UserModule:User:GetEmailsByUserIds"] ??
                    throw new Exception("Route is not added to config file"),
                async ([FromBody] GetEmailsByUserIdsRequest request, [FromServices] IMediator mediator) =>
                await mediator.Send(new GetEmailsByUserIdsQuery(request)))
            .WithTags("User")
            .AllowAnonymous();
    }
}