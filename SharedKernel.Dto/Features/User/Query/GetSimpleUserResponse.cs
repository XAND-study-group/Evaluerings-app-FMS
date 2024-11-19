using SharedKernel.Dto.Features.Semester.Query;

namespace SharedKernel.Dto.Features.User.Query;

public record GetSimpleUserResponse(
    Guid Id,
    string Firstname,
    string Lastname,
    string Email,
    IEnumerable<GetSimpleSemesterResponse> Semesters);

