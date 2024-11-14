using MediatR;
using SharedKernel.Dto.Features.Evaluering.Room.Command;
using SharedKernel.Interfaces;

namespace Module.Feedback.Application.Features.Room.Command;

public record CreateRoomCommand(CreateRoomRequest CreateRoomRequest) : IRequest, ITransactionalCommand;