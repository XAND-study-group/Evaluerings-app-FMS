using MediatR;
using Module.Feedback.Application.Abstractions;
using SharedKernel.Dto.Features.Evaluering.Room.Command;
using SharedKernel.Models;

namespace Module.Feedback.Application.Features.Room.Command;

public record SubscribeToRoomNotificationCommand(SubscribeToRoomNotificationRequest SubscribeToRoomNotificationRequest)
    : IRequest<Result<bool>>;

public class SubscribeToRoomNotificationCommandHandler(IRoomRepository roomRepository)
    : IRequestHandler<SubscribeToRoomNotificationCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(SubscribeToRoomNotificationCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Load
            var subscribeToNotificationRequest = request.SubscribeToRoomNotificationRequest;
            var room = await roomRepository.GetRoomByIdAsync(subscribeToNotificationRequest.RoomId);

            // Do
            room.AddUserIdToNotificationList(subscribeToNotificationRequest.UserId);

            // Save
            await roomRepository.UpdateRoomAsync(room, subscribeToNotificationRequest.RowVersion);

            return Result<bool>.Create("Bruger tilføjet til notifikations liste", true, ResultStatus.Added);
        }
        catch (Exception e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}