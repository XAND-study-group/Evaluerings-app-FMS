using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Feedback.Application.Features.Vote.Query;
using Module.Feedback.Infrastructure.DbContexts;
using SharedKernel.Dto.Features.Evaluering.Feedback.Query;
using SharedKernel.Models;

namespace Module.Feedback.Infrastructure.QueryHandlers.Vote;

public class GetVotesByFeedbackIdQueryHandler(FeedbackDbContext feedbackDbContext, IMapper mapper)
    : IRequestHandler<GetVotesByFeedbackIdQuery, Result<IEnumerable<GetVoteResponse>?>>
{
    async Task<Result<IEnumerable<GetVoteResponse>?>>
        IRequestHandler<GetVotesByFeedbackIdQuery, Result<IEnumerable<GetVoteResponse>?>>.Handle(
            GetVotesByFeedbackIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // Get
            var votes = await feedbackDbContext.Feedbacks
                .AsNoTracking()
                .Include(f => f.Votes)
                .Where(f => f.Id == request.FeedbackId)
                .Select(f => f.Votes.ToArray())
                .SingleAsync(cancellationToken);
            
            // Convert
            var voteResponse = mapper.Map<IEnumerable<GetVoteResponse>>(votes);

            // Return
            return Result<IEnumerable<GetVoteResponse>?>.Create("Votes fundet", voteResponse, ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<GetVoteResponse>?>.Create(e.Message, null, ResultStatus.Error);
        }
    }
}