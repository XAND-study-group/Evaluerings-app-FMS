using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Feedback.Application.Features.Feedback.Query;
using Module.Feedback.Domain;
using Module.Feedback.Infrastructure.DbContexts;
using SharedKernel.Dto.Features.Evaluering.Feedback.Query;
using SharedKernel.Models;
using GetVoteResponse = SharedKernel.Dto.Features.Evaluering.Room.Query.GetVoteResponse;

namespace Module.Feedback.Infrastructure.QueryHandlers.Feedback;

public class GetFeedbacksByRoomIdQueryHandler : IRequestHandler<GetFeedbacksByRoomIdQuery, Result<IEnumerable<GetAllFeedbacksResponse>?>>
{
    private readonly FeedbackDbContext _feedbackDbContext;
    private readonly IMapper _mapper;

    public GetFeedbacksByRoomIdQueryHandler(FeedbackDbContext feedbackDbContext)
    {
        _feedbackDbContext = feedbackDbContext;
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Domain.Feedback, GetAllFeedbacksResponse>();
            cfg.CreateMap<Domain.Comment, GetCommentResponse>();
            cfg.CreateMap<Domain.Vote, GetVoteResponse>();
        }).CreateMapper();
    }
    async Task<Result<IEnumerable<GetAllFeedbacksResponse>?>>
        IRequestHandler<GetFeedbacksByRoomIdQuery, Result<IEnumerable<GetAllFeedbacksResponse>?>>.Handle(
            GetFeedbacksByRoomIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var feedbacks = await _feedbackDbContext.Rooms
                .Include(r => r.Feedbacks)
                .Where(r => r.Id == request.GetFeedbacksByRoomIdRequest.RoomId)
                .Select(r => r.Feedbacks)
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