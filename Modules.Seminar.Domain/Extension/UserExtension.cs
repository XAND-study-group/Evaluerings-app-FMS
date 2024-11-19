using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module.Semester.Domain.Entities;
using SharedKernel.Dto.Features.User.Query;

namespace Module.Semester.Domain.Extension
{
    public static class UserExtension
    {
        public static GetUserResponse MapToGetUserResponse(this User user) =>
            new GetUserResponse(user.Id, user., user.Lastname, user.Email, user.Semesters.Select(s => s.MapToGetSemesterResponse()));

        public static IEnumerable<GetUserResponse> MapToIEnumerableGetUserResponse(this IEnumerable<User> users) =>
        users.Select(u => u.MapToGetUserResponse());
    }
}
