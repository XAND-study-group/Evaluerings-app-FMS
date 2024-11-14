using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Feedback.Application.Features.Room.Query;
using Module.Feedback.Domain;
using Module.Feedback.Infrastructure.DbContexts;
using SharedKernel.Dto.Features.Evaluering.Room.Query;
using SharedKernel.Models;

namespace Module.Feedback.Infrastructure.QueryHandlers;

public class GetAllRoomsQueryHandler : IRequestHandler<GetAllRoomsQuery, Result<IEnumerable<GetAllRoomsResponse>>>
{
    private readonly FeedbackDbContext _feedbackDbContext;
    private readonly IMapper _mapper;

    public GetAllRoomsQueryHandler(FeedbackDbContext feedbackDbContext)
    {
        _feedbackDbContext = feedbackDbContext;
        _mapper = new MapperConfiguration(cfg => { cfg.CreateMap<Room, GetAllRoomsResponse>(); }).CreateMapper();
    }

    async Task<Result<IEnumerable<GetAllRoomsResponse>>>
        IRequestHandler<GetAllRoomsQuery, Result<IEnumerable<GetAllRoomsResponse>>>.Handle(GetAllRoomsQuery request,
            CancellationToken cancellationToken)
    {
        try
        {
            var response = await _feedbackDbContext.Rooms
                .ProjectTo<GetAllRoomsResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return Result<IEnumerable<GetAllRoomsResponse>>.Create("Forums fundet", response, ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<GetAllRoomsResponse>>.Create(e.Message, [], ResultStatus.Error);
        }
    }
}