using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.ExitSlip.Application.Features.ExitSlip.Query;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.ExitSlip.Endpoints.ExitSlip
{
    public class GetAllExitSlips : IEndpoint
    {
        // TODO: FLytte Url til ConfigFil og tilføje Policies.
        void IEndpoint.MapEndpoint(WebApplication app, IConfiguration configuration)
        {
            app.MapGet("ExitSlip",
                async ([FromServices] IMediator mediator) => (await mediator.Send(new GetAllExitSlipsQuery())).ReturnHttpResult())
                  .WithTags("ExitSlip")
                  .RequireAuthorization();
        }
    }
}
