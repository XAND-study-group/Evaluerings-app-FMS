using Microsoft.AspNetCore.Authorization;
using School.Application.Abstractions.Semester;

namespace School.API.AuthorizationHandlers;

public class AssureUserHasSubjectRequirement : IAuthorizationRequirement
{
    public AssureUserHasSubjectRequirement(params string[] roles)
    {
        Roles = roles;
    }

    public string[] Roles { get; }
}

public class AssureUserHasSubject(ISubjectRepository subjectRepository)
    : AuthorizationHandler<AssureUserHasSubjectRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        AssureUserHasSubjectRequirement requirement)
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

        var subjectIdStr = request?.Request.RouteValues["subjectId"]?.ToString() ?? "";
        var isSubjectIdParsed = Guid.TryParse(subjectIdStr, out var subjectId);

        if (!isSubjectIdParsed || !isUserIdParsed)
        {
            context.Fail();
            return;
        }

        var doesUserHaveSubject = await subjectRepository.DoesUserHaveSubject(subjectId, userId);

        if (!doesUserHaveSubject)
        {
            context.Fail();
            return;
        }

        context.Succeed(requirement);
    }
}