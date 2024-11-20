using SharedKernel.Dto.Features.School.Semester.Query;

namespace SharedKernel.Dto.Features.School.User.Query;

public record GetUsersResponse(
    Guid Id,
    string Firstname,
    string Lastname,
    string Email,
    IEnumerable<GetSemestersResponse> Semesters);