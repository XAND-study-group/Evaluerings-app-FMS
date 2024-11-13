using SharedKernel.Dto.Features.Semester.Query;

namespace SharedKernel.Dto.Features.User.Query;

public record GetUserFullResponse(
    Guid Id,
    string Firstname,
    string Lastname,
    string Email,
    IEnumerable<GetSemestersResponse> Semesters);

