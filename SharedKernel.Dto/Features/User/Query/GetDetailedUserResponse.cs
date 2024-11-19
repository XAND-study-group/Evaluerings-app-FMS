using SharedKernel.Dto.Features.Semester.Query;

namespace SharedKernel.Dto.Features.User.Query;

public record GetDetailedUserResponse(
    Guid Id,
    string Firstname,
    string Lastname,
    string Email,
    IEnumerable<GetSimpleSemesterResponse> Semesters);