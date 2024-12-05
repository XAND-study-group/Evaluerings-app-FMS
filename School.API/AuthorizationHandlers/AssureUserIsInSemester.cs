using Microsoft.AspNetCore.Authorization;
using School.Application.Abstractions.Semester;

namespace School.API.AuthorizationHandlers;

public class AssureUserIsInSemesterRequirement : IAuthorizationRequirement
{
    public string[] Roles { get; }

    public AssureUserIsInSemesterRequirement(params string[] roles)
    {
        Roles = roles;
    }
}

public class AssureUserIsInSemester(ISemesterRepository semesterRepository) : AuthorizationHandler<AssureUserIsInSemesterRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AssureUserIsInSemesterRequirement requirement)
    {
        var userIdStr = context.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value ?? string.Empty;
        var role = context.User.FindFirst("Role")?.Value ?? string.Empty;

        if (requirement.Roles.Contains(role))
        {
            context.Succeed(requirement);
            return;
        }
        
        var isUserIdParsed = Guid.TryParse(userIdStr, out var userId);
        
        var request = context.Resource as HttpContext;
        
        var semesterIdStr = request?.Request.RouteValues["semesterId"]?.ToString() ?? "";
        var isSemesterIdParsed = Guid.TryParse(semesterIdStr, out var semesterId);
        
        if (!isSemesterIdParsed || !isUserIdParsed)
        {
            context.Fail();
            return;
        }

        var isUserInSemester = await semesterRepository.IsStudentInSemester(semesterId, userId);

        if (!isUserInSemester)
        {
            context.Fail();
            return;
        }
        
        context.Succeed(requirement);
    }
}