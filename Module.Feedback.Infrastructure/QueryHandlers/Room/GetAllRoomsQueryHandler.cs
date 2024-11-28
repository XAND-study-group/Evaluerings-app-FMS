using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Feedback.Application.Features.Room.Query;
using Module.Feedback.Infrastructure.DbContexts;
using SharedKernel.Dto.Features.Evaluering.Room.Query;
using SharedKernel.Models;

namespace Module.Feedback.Infrastructure.QueryHandlers.Room;

public class GetAllRoomsQueryHandler : IRequestHandler<GetAllRoomsQuery, Result<IEnumerable<GetSimpleRoomResponse>>>
{
    private readonly FeedbackDbContext _feedbackDbContext;
    private readonly IMapper _mapper;

    public GetAllRoomsQueryHandler(FeedbackDbContext feedbackDbContext)
    {
        _feedbackDbContext = feedbackDbContext;
        _mapper = new MapperConfiguration(cfg => 
            { cfg.CreateMap<Domain.Room, GetSimpleRoomResponse>(); }).CreateMapper();
    }

    async Task<Result<IEnumerable<GetSimpleRoomResponse>>>
        IRequestHandler<GetAllRoomsQuery, Result<IEnumerable<GetSimpleRoomResponse>>>.Handle(GetAllRoomsQuery request,
            CancellationToken cancellationToken)
    {
        try
        {
            var response = await _feedbackDbContext.Rooms
                .AsNoTracking()
                .ProjectTo<GetSimpleRoomResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return Result<IEnumerable<GetSimpleRoomResponse>>.Create("Forums fundet", response, ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<GetSimpleRoomResponse>>.Create(e.Message, [], ResultStatus.Error);
        }
    }
}