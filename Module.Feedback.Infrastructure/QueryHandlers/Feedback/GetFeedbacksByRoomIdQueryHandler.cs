using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Feedback.Application.Features.Feedback.Query;
using Module.Feedback.Infrastructure.DbContexts;
using SharedKernel.Dto.Features.Evaluering.Feedback.Query;
using SharedKernel.Models;

namespace Module.Feedback.Infrastructure.QueryHandlers.Feedback;

public class GetFeedbacksByRoomIdQueryHandler : IRequestHandler<GetFeedbacksByRoomIdQuery, Result<IEnumerable<GetAllFeedbacksResponse>?>>
{
    private readonly FeedbackDbContext _feedbackDbContext;
    private readonly IMapper _mapper;

    public GetFeedbacksByRoomIdQueryHandler(FeedbackDbContext feedbackDbContext, IMapper mapper)
    {
        _feedbackDbContext = feedbackDbContext;
        _mapper = mapper;
    }
    async Task<Result<IEnumerable<GetAllFeedbacksResponse>?>>
        IRequestHandler<GetFeedbacksByRoomIdQuery, Result<IEnumerable<GetAllFeedbacksResponse>?>>.Handle(
            GetFeedbacksByRoomIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var feedbacks = await _feedbackDbContext.Feedbacks
                .AsNoTracking()
                .Include(f => f.Room)
                .Where(f => f.Room.Id == request.RoomId)
                .Skip(request.ItemsPerPage * (request.Page - 1))
                .Take(request.ItemsPerPage)
                .ProjectTo<GetAllFeedbacksResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return Result<IEnumerable<GetAllFeedbacksResponse>?>.Create("Alle evalueringer tilknyttet det specifikke forum fundet",
                feedbacks, ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<GetAllFeedbacksResponse>?>.Create(e.Message, null, ResultStatus.Error);
        }
    }
}