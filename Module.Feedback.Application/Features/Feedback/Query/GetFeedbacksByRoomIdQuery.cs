using MediatR;
using SharedKernel.Dto.Features.Evaluering.Feedback.Query;
using SharedKernel.Models;

namespace Module.Feedback.Application.Features.Feedback.Query;

public record GetFeedbacksByRoomIdQuery(Guid RoomId, int Page, int ItemsPerPage)
    : IRequest<Result<IEnumerable<GetAllFeedbacksResponse>?>>;