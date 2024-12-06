using Microsoft.AspNetCore.Authorization;
using School.Application.Abstractions.Semester;

namespace School.API.AuthorizationHandlers;

public class AssureUserIsInClassRequirement : IAuthorizationRequirement
{
    public AssureUserIsInClassRequirement(params string[] roles)
    {
        Roles = roles;
    }

    public string[] Roles { get; }
}

public class AssureUserIsInClass(IClassRepository classRepository)
    : AuthorizationHandler<AssureUserIsInClassRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        AssureUserIsInClassRequirement requirement)
    {
        var userIdStr =
            context.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value ??
            string.Empty;
        var role = context.User.FindFirst("Role")?.Value ?? string.Empty;

        if (requirement.Roles.Contains(role))
        {
            context.Succeed(requirement);
            return;
        }

        var isUserIdParsed = Guid.TryParse(userIdStr, out var userId);

        var request = context.Resource as HttpContext;

        var classIdStr = request?.Request.RouteValues["classId"]?.ToString() ?? "";
        var isClassIdParsed = Guid.TryParse(classIdStr, out var classId);

        if (!isClassIdParsed || !isUserIdParsed)
        {
            context.Fail();
            return;
        }

        var isUserInClass = await classRepository.IsUserInClass(classId, userId);

        if (!isUserInClass)
        {
            context.Fail();
            return;
        }

        context.Succeed(requirement);
    }
}