using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Module.Feedback.Application.Features.Room.Command;
using SharedKernel.Dto.Features.Evaluering.Feedback.Command;

namespace Module.Feedback.AuthorizationHandlers;

public class AssureUserInRoomOfFeedbackModificationRequirement : IAuthorizationRequirement
{
    public AssureUserInRoomOfFeedbackModificationRequirement(params string[] roles)
    {
        Roles = roles;
    }

    public string[] Roles { get; }
}

public class AssureUserInRoomOfFeedbackModification(IMediator mediator)
    : AuthorizationHandler<AssureUserInRoomOfFeedbackModificationRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        AssureUserInRoomOfFeedbackModificationRequirement requirement)
    {
        var role = context.User.FindFirst("Role")?.Value ?? string.Empty;

        if (requirement.Roles.Contains(role))
        {
            context.Succeed(requirement);
            return;
        }

        var request = context.Resource as HttpContext;

        var userClasses = context.User.FindAll("Class").Select(c => Guid.Parse(c.Value));

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            IgnoreNullValues = true
        };

        var body = await new StreamReader(request.Request.Body).ReadToEndAsync();
        request.Request.Body.Position = 0;
        var requestBody = JsonSerializer.Deserialize<FeedbackIdRequest>(body, options);

        if (requestBody is null)
        {
            context.Fail();
            return;
        }

        var isUserInRoom =
            await mediator.Send(new IsUserInRoomUpdateFeedbackCommand(requestBody.FeedbackId, userClasses));

        if (!isUserInRoom.SuccessResult)
        {
            context.Fail();
            return;
        }

        context.Succeed(requirement);
    }
}