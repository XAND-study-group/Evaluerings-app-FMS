using MediatR;
using Module.Feedback.Application.Abstractions;
using SharedKernel.Dto.Features.Evaluering.Room.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace Module.Feedback.Application.Features.Room.Command;

public record DeleteRoomCommand(DeleteRoomRequest DeleteRoomRequest) : IRequest<Result<bool>>, ITransactionalCommand;

public class DeleteRoomCommandHandler(IRoomRepository roomRepository) : IRequestHandler<DeleteRoomCommand, Result<bool>>
{
    async Task<Result<bool>> IRequestHandler<DeleteRoomCommand, Result<bool>>.Handle(DeleteRoomCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Load
            var deleteRoomRequest = request.DeleteRoomRequest;
            var roomToDelete = await roomRepository.GetRoomByIdAsync(deleteRoomRequest.RoomId);

            // Do & Save
            await roomRepository.DeleteRoomAsync(roomToDelete, deleteRoomRequest.RowVersion);

            return Result<bool>.Create("Forum fjernet", true, ResultStatus.Deleted);
        }
        catch (Exception e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}