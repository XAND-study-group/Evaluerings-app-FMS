namespace SharedKernel.Dto.Features.School.Class.Query;

public record GetDetailedClassResponse(
    Guid Id,
    byte[] RowVersion,
    string Name,
    string Description,
    int StudentCapacity,
    IEnumerable<GetClassUserResponse> Teachers,
    IEnumerable<GetClassUserResponse> Students,
    IEnumerable<GetClassSubjectResponse> Subjects);