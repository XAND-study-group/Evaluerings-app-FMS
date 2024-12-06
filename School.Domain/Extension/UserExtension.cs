using School.Domain.Entities;
using SharedKernel.Dto.Features.School.User.Query;

namespace School.Domain.Extension;

public static class UserExtension
{
    public static GetSimpleUserResponse MapToGetSimpleUserResponse(this User user)
    {
        return new GetSimpleUserResponse(user.Id, user.Firstname, user.Lastname, user.Email,
            user.Semesters.Select(s => s.MapToGetSimpleSemesterResponse()));
    }

    public static IEnumerable<GetSimpleUserResponse> MapToIEnumerableGetUserResponse(this IEnumerable<User> users)
    {
        return users.Select(u => u.MapToGetSimpleUserResponse());
    }
}