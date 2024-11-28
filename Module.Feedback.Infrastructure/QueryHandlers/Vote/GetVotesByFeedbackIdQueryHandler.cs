using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Feedback.Application.Features.Vote.Query;
using Module.Feedback.Domain.Extensions;
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
            // Load
            var votes = await feedbackDbContext.Feedbacks
                .AsNoTracking()
                .Include(f => f.Votes)
                .Where(f => f.Id == request.FeedbackId)
                .Select(f => f.Votes)
                .ProjectTo<IEnumerable<GetVoteResponse>>(mapper.ConfigurationProvider)
                .SingleAsync(cancellationToken);


            return Result<IEnumerable<GetVoteResponse>?>.Create("Votes fundet", votes, ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<GetVoteResponse>?>.Create(e.Message, null, ResultStatus.Error);
        }
    }
}