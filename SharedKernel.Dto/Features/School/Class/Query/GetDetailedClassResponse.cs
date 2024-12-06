namespace SharedKernel.Dto.Features.School.Class.Query;

public record GetDetailedClassResponse(
    Guid Id,
    byte[] RowVersion,
    string Name,
    string Description,
    int StudentCapacity,
    IEnumerable<GetClassUserResponse> Teachers,
    IEnumerable<GetClassUserResponse> Students,
    IEnumerable<GetClassSubjectResponse> Subjects)
{
    public GetDetailedClassResponse() : this(Guid.Empty, Array.Empty<byte>(), string.Empty, string.Empty, 0, new List<GetClassUserResponse>(), new List<GetClassUserResponse>(), new List<GetClassSubjectResponse>()) { }
}