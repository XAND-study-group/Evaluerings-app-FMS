namespace SharedKernel.Dto.Features.Class.Query;

public record GetClassResponse(
    Guid Id,
    byte[] RowVersion,
    string Name,
    string Description,
    int StudentCapacity,
    IEnumerable<GetClassUserResponse> Teachers,
    IEnumerable<GetClassUserResponse> Students,
    IEnumerable<GetClassSubjectResponse> Subjects);