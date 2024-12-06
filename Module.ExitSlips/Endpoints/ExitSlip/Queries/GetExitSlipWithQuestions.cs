using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.ExitSlip.Application.Features.ExitSlip.Query;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.ExitSlip.Endpoints.ExitSlip.Queries
{
    public class GetExitSlipWithQuestions : IEndpoint
    {
        void IEndpoint.MapEndpoint(WebApplication app, IConfiguration configuration)
        {
            app.MapGet(configuration["Routes:ExitSlipModule:ExitSlip:GetExitSlipWithQuestions"] ??
                     throw new ArgumentException("Route is not added to config file"),
              async ([FromRoute] Guid exitSlipId, [FromServices] IMediator mediator) =>
              (await mediator.Send(new GetExitSlipWithQuestionsQuery(exitSlipId))).ReturnHttpResult())
              .WithTags("ExitSlip")
              .RequireAuthorization();
        }
    }
}
