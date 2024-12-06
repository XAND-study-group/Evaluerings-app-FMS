using MediatR;
using Module.Feedback.Application.Abstractions;
using SharedKernel.Dto.Features.Evaluering.Room.Command;
using SharedKernel.Models;

namespace Module.Feedback.Application.Features.Room.Command;

public record RemoveClassIdFromRoomCommand(RemoveClassIdFromRoomRequest RemoveClassIdFromRoomRequest)
    : IRequest<Result<bool>>;

public class RemoveClassIdFromRoomCommandHandler(IRoomRepository roomRepository)
    : IRequestHandler<RemoveClassIdFromRoomCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(RemoveClassIdFromRoomCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Load
            var removeClassIdFromRoomRequest = request.RemoveClassIdFromRoomRequest;
            var room = await roomRepository.GetRoomByIdAsync(removeClassIdFromRoomRequest.RoomId);

            // Do
            room.RemoveClassId(removeClassIdFromRoomRequest.ClassId);

            // Save
            await roomRepository.UpdateRoomAsync(room, removeClassIdFromRoomRequest.RowVersion);

            return Result<bool>.Create("Klasse fjernet fra forum", true, ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}