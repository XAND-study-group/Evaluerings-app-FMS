using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Feedback.Application.Features.Vote.Query;
using Module.Feedback.Domain.Extensions;
using Module.Feedback.Infrastructure.DbContexts;
using SharedKernel.Dto.Features.Evaluering.Feedback.Query;
using SharedKernel.Models;

namespace Module.Feedback.Infrastructure.QueryHandlers.Vote;

public class GetVotesByFeedbackIdQueryHandler(FeedbackDbContext feedbackDbContext) : IRequestHandler<GetVotesByFeedbackIdQuery, Result<IEnumerable<GetVoteResponse>?>>
{
    async Task<Result<IEnumerable<GetVoteResponse>?>>
        IRequestHandler<GetVotesByFeedbackIdQuery, Result<IEnumerable<GetVoteResponse>?>>.Handle(
            GetVotesByFeedbackIdQuery request, CancellationToken cancellationToken)
    {
        try
        {

            // Load
            var getVotesRequest = request.GetVotesByFeedbackIdRequest;
            var feedback = await feedbackDbContext.Feedbacks
                .SingleAsync(f => f.Id == getVotesRequest.FeedbackId, cancellationToken);
        
            List<GetVoteResponse> dtoVotes = [];
            foreach (Domain.Vote vote in feedback.Votes)
            {
                dtoVotes.Add(vote.MapToGetVoteResponse());
            }

            return Result<IEnumerable<GetVoteResponse>?>.Create("Votes fundet", dtoVotes, ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<GetVoteResponse>?>.Create(e.Message, null, ResultStatus.Error);
        }
    }
}