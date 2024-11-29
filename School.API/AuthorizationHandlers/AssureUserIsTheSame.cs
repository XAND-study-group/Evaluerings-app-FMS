using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using SharedKernel.Dto.Features.School.User.Command;

namespace School.API.AuthorizationHandlers;

public class AssureUserIsTheSameRequirement : IAuthorizationRequirement
{
    public string[] Roles { get; }

    public AssureUserIsTheSameRequirement(params string[] roles)
    {
        Roles = roles;
    }
}

public class AssureUserIsTheSame : AuthorizationHandler<AssureUserIsTheSameRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        AssureUserIsTheSameRequirement requirement)
    {
        var tokenUserIdStr = context.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value ?? string.Empty;
        var role = context.User.FindFirst("Role")?.Value ?? string.Empty;

        var request = context.Resource as HttpContext;
        
        var userId = request?.Request.RouteValues["userId"]?.ToString();

        if (string.IsNullOrEmpty(userId))
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                IgnoreNullValues = true
            };

            var body = await new StreamReader(request.Request.Body).ReadToEndAsync();
            request.Request.Body.Position = 0;
            userId = JsonSerializer.Deserialize<UserIdRequest>(body, options)?.UserId.ToString();
        }

        if (tokenUserIdStr != userId && !requirement.Roles.Contains(role))
        {
            context.Fail(new AuthorizationFailureReason(this, "Du prøver at ændre en anden brugers adgangskode."));
            return;
        }

        context.Succeed(requirement); // User is authorized
    }
}