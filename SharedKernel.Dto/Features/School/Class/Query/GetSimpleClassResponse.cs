namespace SharedKernel.Dto.Features.School.Class.Query;

public record GetSimpleClassResponse(
    Guid Id,
    byte[] RowVersion,
    string Name,
    string Description,
    int StudentCapacity)
{
    public GetSimpleClassResponse() : this(Guid.Empty, Array.Empty<byte>(), string.Empty, string.Empty, 0)
    {
    }
}