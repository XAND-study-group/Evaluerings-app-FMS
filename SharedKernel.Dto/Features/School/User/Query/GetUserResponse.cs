using SharedKernel.Dto.Features.School.Semester.Query;

namespace SharedKernel.Dto.Features.School.User.Query;

public record GetUserResponse(
    Guid Id,
    string Firstname,
    string Lastname,
    string Email,
    IEnumerable<GetSemestersResponse> Semesters);

