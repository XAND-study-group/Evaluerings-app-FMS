using MediatR;
using Module.Feedback.Application.Abstractions;
using SharedKernel.Dto.Features.Evaluering.Room.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace Module.Feedback.Application.Features.Room.Command;

public record CreateRoomCommand(CreateRoomRequest CreateRoomRequest) : IRequest<Result<bool>>, ITransactionalCommand;

public class CreateRoomCommandHandler(IRoomRepository roomRepository) : IRequestHandler<CreateRoomCommand, Result<bool>>
{
    async Task<Result<bool>> IRequestHandler<CreateRoomCommand, Result<bool>>.Handle(CreateRoomCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Load
            var createRoomRequest = request.CreateRoomRequest;

            // Do
            var room = Domain.Entities.Room.Create(createRoomRequest.Title, createRoomRequest.Description);

            // Save
            await roomRepository.CreateRoomAsync(room);

            return Result<bool>.Create("Forum oprettet", true, ResultStatus.Created);
        }
        catch (ArgumentException e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}