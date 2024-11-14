using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Semester.Application.Features.Class.Query;
using Module.Semester.Infrastructure.DbContexts;
using SharedKernel.Dto.Features.Class.Query;
using SharedKernel.Models;

namespace Module.Semester.Infrastructure.Features.Class;

public class GetClassesByUserIdQueryHandler : IRequestHandler<GetClassesByUserIdQuery, Result<IEnumerable<GetClassesResponse>>>
{
    private readonly SemesterDbContext _semesterDbContext;
    private readonly IMapper _mapper;

    public GetClassesByUserIdQueryHandler(SemesterDbContext semesterDbContext)
    {
        _semesterDbContext = semesterDbContext;
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Domain.Entities.Class, GetClassesResponse>();
        }).CreateMapper();
    }


    async Task<Result<IEnumerable<GetClassesResponse>>> IRequestHandler<GetClassesByUserIdQuery,
        Result<IEnumerable<GetClassesResponse>>>.Handle(GetClassesByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var getClassesResponse = await _semesterDbContext.Classes
                .AsNoTracking()
                .Where(s => s.Students.Any(st => st.Id == request.UserId))
                .ProjectTo<GetClassesResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return Result<IEnumerable<GetClassesResponse>>.Create("Klasser tilhørende bruger fundet", getClassesResponse,
                ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<GetClassesResponse>>.Create(e.Message, [],
                ResultStatus.Error);
        }
    }
        
}