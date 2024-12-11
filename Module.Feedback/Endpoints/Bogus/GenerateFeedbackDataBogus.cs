using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.Feedback.Application.Features.Bogus;
using SharedKernel.Interfaces;

namespace Module.Feedback.Endpoints.Bogus
{
    public class GenerateFeedbackDataBogus : IEndpoint
    {
        void IEndpoint.MapEndpoint(WebApplication app, IConfiguration configuration)
        {
            app.MapPost(configuration["Routes:FeedbackModule:Bogus:GenerateFeedbackDataBogus"] ??
                   throw new ArgumentException("Route is not added to config file"),
           async ([FromServices] IMediator mediator) =>
           await mediator.Send(new GenerateFeedBackDataBogusCommand())).WithTags("GenerateData");
        }
    }
}
