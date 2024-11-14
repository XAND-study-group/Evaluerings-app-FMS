using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Semester.Application.Features.Lecture.Query;
using Module.Semester.Domain.Entities;
using Module.Semester.Infrastructure.DbContexts;
using SharedKernel.Dto.Features.Lecture.Query;
using SharedKernel.Models;

namespace Module.Semester.Infrastructure.Features.Lecture;

public class GetLectureQueryHandler : IRequestHandler<GetLectureQuery, Result<GetLectureResponse?>>
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

    async Task<Result<GetLectureResponse?>> IRequestHandler<GetLectureQuery, Result<GetLectureResponse?>>.Handle(GetLectureQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var getLectureResponse = await _dbContext.Lectures
                .AsNoTracking()
                .Where(l => l.Id == request.lectureId)
                .ProjectTo<GetLectureResponse>(_mapper.ConfigurationProvider)
                .SingleAsync(cancellationToken);

            return Result<GetLectureResponse?>.Create("Specifikke Lektion fundet", getLectureResponse,
                ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<GetLectureResponse?>.Create(e.Message, null,
                ResultStatus.Error);
        }
    }
}