using MediatR;
using Module.Feedback.Application.Abstractions;
using SharedKernel.Dto.Features.Evaluering.Room.Command;
using SharedKernel.Models;

namespace Module.Feedback.Application.Features.Room.Command;

public record UnsubscribeFromRoomNotificationCommand(
    UnsubscribeToRoomNotificationRequest UnsubscribeToRoomNotificationRequest) : IRequest<Result<bool>>;

public class UnsubscribeFromRoomNotificationCommandHandler(IRoomRepository roomRepository)
    : IRequestHandler<UnsubscribeFromRoomNotificationCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UnsubscribeFromRoomNotificationCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Load
            var unsubscribeToRoomNotificationRequest = request.UnsubscribeToRoomNotificationRequest;
            var room = await roomRepository.GetRoomByIdAsync(unsubscribeToRoomNotificationRequest.RoomId);

            // Do
            room.RemoveUserIdFromNotificationList(unsubscribeToRoomNotificationRequest.UserId);

            // Save
            await roomRepository.UpdateRoomAsync(room, unsubscribeToRoomNotificationRequest.RowVersion);

            return Result<bool>.Create("Bruger fjernet fra notifikations listen", true, ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}