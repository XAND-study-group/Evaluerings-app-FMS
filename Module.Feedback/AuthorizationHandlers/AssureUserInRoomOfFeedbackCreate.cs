﻿using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Module.Feedback.Application.Features.Room.Command;
using SharedKernel.Dto.Features.Evaluering.Feedback.Command;

namespace Module.Feedback.AuthorizationHandlers;

public class AssureUserInRoomOfFeedbackCreateRequirement : IAuthorizationRequirement
{
    public AssureUserInRoomOfFeedbackCreateRequirement(params string[] roles)
    {
        Roles = roles;
    }

    public string[] Roles { get; }
}

public class AssureUserInRoomOfFeedbackCreate(IMediator mediator)
    : AuthorizationHandler<AssureUserInRoomOfFeedbackCreateRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        AssureUserInRoomOfFeedbackCreateRequirement requirement)
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
        var requestBody = JsonSerializer.Deserialize<CreateFeedbackRequest>(body, options);

        if (requestBody is null)
        {
            context.Fail();
            return;
        }

        var isUserInRoom = await mediator.Send(new IsUserInRoomCreateFeedbackCommand(requestBody.RoomId, userClasses));

        if (!isUserInRoom.SuccessResult)
        {
            context.Fail();
            return;
        }

        context.Succeed(requirement);
    }
}