﻿using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.ExitSlip.Application.Features.Question.Command;
using SharedKernel.Dto.Features.Evaluering.Question.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.ExitSlip.Endpoints.Question;

public class DeleteQuestion : IEndpoint
{
    public void MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapDelete(configuration["Routes:ExitSlipModule:Question:DeleteQuestion"] ??
                      throw new Exception("Route is not added to config file"),
                async ([FromBody] DeleteQuestionRequest deleteQuestionRequest, [FromServices] IMediator mediator) =>
                (await mediator.Send(new DeleteQuestionCommand(deleteQuestionRequest))).ReturnHttpResult())
            .WithTags("Question")
            .RequireAuthorization();
    }
}