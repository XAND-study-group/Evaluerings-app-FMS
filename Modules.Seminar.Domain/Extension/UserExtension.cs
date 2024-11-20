using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module.Semester.Domain.Entities;
using SharedKernel.Dto.Features.School.User.Query;

namespace Module.Semester.Domain.Extension
{
    public static class UserExtension
    {
        public static GetSimpleUserResponse MapToGetSimpleUserResponse(this User user) =>
            new GetSimpleUserResponse(user.Id, user.Firstname, user.Lastname, user.Email, user.Semesters.Select(s => s.MapToGetSimpleSemesterResponse()));

        public static IEnumerable<GetSimpleUserResponse> MapToIEnumerableGetUserResponse(this IEnumerable<User> users) =>
        users.Select(u => u.MapToGetSimpleUserResponse());
    }
}
