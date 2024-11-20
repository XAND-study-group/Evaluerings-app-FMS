using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Semester.Application.Features.Semester.Query;
using Module.Semester.Infrastructure.DbContexts;
using SharedKernel.Dto.Features.School.Semester.Query;
using SharedKernel.Models;

namespace Module.Semester.Infrastructure.Features.Semester;

public class GetSemesterQueryHandler : IRequestHandler<GetSemesterQuery, Result<GetSemesterResponse?>>
{
    private readonly SemesterDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetSemesterQueryHandler(SemesterDbContext dbContext)
    {
        _dbContext = dbContext;
        _mapper = new MapperConfiguration(cfg => { cfg.CreateMap<Domain.Entities.Semester, GetSemesterResponse>(); })
            .CreateMapper();
    }

    async Task<Result<GetSemesterResponse?>> IRequestHandler<GetSemesterQuery, Result<GetSemesterResponse?>>.Handle(
        GetSemesterQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var getSemesterResponse = await _dbContext.Semesters
                .AsNoTracking()
                .Where(s => s.Id == request.SemesterId)
                .ProjectTo<GetSemesterResponse>(_mapper.ConfigurationProvider)
                .SingleAsync(cancellationToken);

            return Result<GetSemesterResponse?>.Create("Efterspurgte Semester fundet", getSemesterResponse,
                ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<GetSemesterResponse?>.Create(e.Message, null,
                ResultStatus.Error);
        }
    }
}