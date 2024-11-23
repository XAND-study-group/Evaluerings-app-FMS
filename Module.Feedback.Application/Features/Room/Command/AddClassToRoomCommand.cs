using MediatR;
using Module.Feedback.Application.Abstractions;
using SharedKernel.Dto.Features.Evaluering.Room.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace Module.Feedback.Application.Features.Room.Command;

public record AddClassToRoomCommand(AddClassToRoomRequest AddClassToRoomRequest)
    : IRequest<Result<bool>>, ITransactionalCommand;

public class AddClassToRoomCommandHandler(IRoomRepository roomRepository)
    : IRequestHandler<AddClassToRoomCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(AddClassToRoomCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Load
            var addClassToRoomRequest = request.AddClassToRoomRequest;
            var room = await roomRepository.GetRoomByIdAsync(addClassToRoomRequest.RoomId);

            // Do
            room.AddClassIdAsync(addClassToRoomRequest.ClassId);

            // Save
            await roomRepository.UpdateRoomAsync(room, addClassToRoomRequest.RowVersion);

            return Result<bool>.Create("Klasse tilføjet til forum", true, ResultStatus.Added);
        }
        catch (Exception e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}