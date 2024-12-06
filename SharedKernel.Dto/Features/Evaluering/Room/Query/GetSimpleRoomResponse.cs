namespace SharedKernel.Dto.Features.Evaluering.Room.Query;

public record GetSimpleRoomResponse(
    Guid Id,
    string Title,
    string Description,
    byte[] RowVersion);