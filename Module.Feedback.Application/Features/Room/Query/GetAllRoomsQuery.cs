using MediatR;
using SharedKernel.Dto.Features.Evaluering.Room.Query;
using SharedKernel.Models;

namespace Module.Feedback.Application.Features.Room.Query;

public record GetAllRoomsQuery() : IRequest<Result<IEnumerable<GetAllRoomsResponse>>>;