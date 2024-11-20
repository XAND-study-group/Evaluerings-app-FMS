using MediatR;
using Module.Feedback.Application.Abstractions;
using SharedKernel.Dto.Features.Evaluering.Room.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace Module.Feedback.Application.Features.Room.Command;

public record UpdateRoomCommand(UpdateRoomRequest UpdateRoomRequest) : IRequest<Result<bool>>, ITransactionalCommand;

public class UpdateRoomCommandHandler(IRoomRepository roomRepository) : IRequestHandler<UpdateRoomCommand, Result<bool>>
{
    async Task<Result<bool>> IRequestHandler<UpdateRoomCommand, Result<bool>>.Handle(UpdateRoomCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Load
            var updateRoomRequest = request.UpdateRoomRequest;
            var room = await roomRepository.GetRoomByIdAsync(updateRoomRequest.RoomId);

            // Do
            room.Update(updateRoomRequest.Title, updateRoomRequest.Description);

            // Save
            await roomRepository.UpdateRoomAsync(room, updateRoomRequest.RowVersion);

            return Result<bool>.Create("Forum opdateret", true, ResultStatus.Updated);
        }
        catch (Exception e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}