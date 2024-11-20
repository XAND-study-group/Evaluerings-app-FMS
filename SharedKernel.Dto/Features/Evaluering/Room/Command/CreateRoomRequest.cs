namespace SharedKernel.Dto.Features.Evaluering.Room.Command;

public record CreateRoomRequest(
    string Title,
    string Description);