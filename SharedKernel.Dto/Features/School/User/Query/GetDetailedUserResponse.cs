using SharedKernel.Dto.Features.School.Semester.Query;

namespace SharedKernel.Dto.Features.School.User.Query;

public record GetDetailedUserResponse(
    Guid Id,
    string Firstname,
    string Lastname,
    string Email,
    IEnumerable<GetSimpleSemesterResponse> Semesters)
{
    public GetDetailedUserResponse() : this(Guid.Empty, string.Empty, string.Empty, string.Empty,
        new List<GetSimpleSemesterResponse>())
    {
    }
}