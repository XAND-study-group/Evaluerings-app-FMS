using SharedKernel.Dto.Features.Semester.Query;

namespace SharedKernel.Dto.Features.User.Query;

public record GetUserResponse(
    Guid Id,
    string Firstname,
    string Lastname,
    string Email,
    IEnumerable<GetSemestersResponse> Semesters);