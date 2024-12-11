using MediatR;
using SharedKernel.Dto.Features.Evaluering.Feedback.Query;
using SharedKernel.Models;

namespace Module.Feedback.Application.Features.Vote.Query;

public record GetVotesByFeedbackIdQuery(Guid FeedbackId) : IRequest<Result<IEnumerable<GetDetailedVoteResponse>?>>;