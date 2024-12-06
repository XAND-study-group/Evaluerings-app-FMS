namespace SharedKernel.Dto.Features.Evaluering.Room.Query;

public record GetDetailedRoomResponse(
    Guid Id,
    byte[] RowVersion,
    string Title,
    string Description);