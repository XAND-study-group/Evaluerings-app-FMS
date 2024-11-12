using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Semester.Application.Features.Lecture.Query;
using Module.Semester.Domain.Entities;
using Module.Semester.Infrastructure.DbContexts;
using SharedKernel.Dto.Features.Lecture.Query;

namespace Module.Semester.Infrastructure.Features.Lecture;

public class GetLectureQueryHandler : IRequestHandler<GetLectureQuery, GetLectureResponse>
{
    private readonly SemesterDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetLectureQueryHandler(SemesterDbContext dbContext)
    {
        _dbContext = dbContext;
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Domain.Entities.Lecture, GetLectureResponse>();
            cfg.CreateMap<User, GetLectureUserResponse>();
        }).CreateMapper();
    }

    async Task<GetLectureResponse> IRequestHandler<GetLectureQuery, GetLectureResponse>.Handle(GetLectureQuery request,
        CancellationToken cancellationToken)
        => await _dbContext.Lectures
            .AsNoTracking()
            .Where(l => l.Id == request.lectureId)
            .ProjectTo<GetLectureResponse>(_mapper.ConfigurationProvider)
            .SingleAsync(cancellationToken);
}