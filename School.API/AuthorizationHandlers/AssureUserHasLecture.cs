using Microsoft.AspNetCore.Authorization;
using School.Application.Abstractions.Semester;

namespace School.API.AuthorizationHandlers;

public class AssureUserHasLectureRequirement : IAuthorizationRequirement
{
    public string[] Roles { get; }

    public AssureUserHasLectureRequirement(params string[] roles)
    {
        Roles = roles;
    }
}

public class AssureUserHasLecture(ILectureRepository lectureRepository) : AuthorizationHandler<AssureUserHasLectureRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AssureUserHasLectureRequirement requirement)
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
        
        var lectureIdStr = request?.Request.RouteValues["lectureId"]?.ToString() ?? "";
        var isLectureIdParsed = Guid.TryParse(lectureIdStr, out var lectureId);
        
        if (!isLectureIdParsed || !isUserIdParsed)
        {
            context.Fail();
            return;
        }
        
        var doesUserHaveLecture = await lectureRepository.DoesUserHaveLecture(lectureId, userId);

        if (!doesUserHaveLecture)
        {
            context.Fail();
            return;
        }
        
        context.Succeed(requirement);
    }
}