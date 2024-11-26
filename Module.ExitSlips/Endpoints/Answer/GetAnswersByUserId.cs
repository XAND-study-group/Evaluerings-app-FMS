using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.ExitSlip.Application.Features.Answer.Query;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.ExitSlip.Endpoints.Answer
{
    public class GetAnswersByUserId : IEndpoint
    {
        public void MapEndpoint(WebApplication app, IConfiguration configuration)
        {
            app.MapGet("Exitslip/Question/Answer/{UserId:guid}",
                async([FromRoute] Guid UserId, [FromServices] IMediator mediator)=>
                (await mediator.Send(new GetAnswersByUserIdQuery(UserId))).ReturnHttpResult())
                .WithTags("Answer")
                .RequireAuthorization();
        }
    }
}
