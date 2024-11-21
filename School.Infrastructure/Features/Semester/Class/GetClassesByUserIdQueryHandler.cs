﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Features.SemesterFeature.Class.Query;
using School.Infrastructure.DbContext;
using SharedKernel.Dto.Features.School.Class.Query;
using SharedKernel.Models;

namespace School.Infrastructure.Features.Semester.Class;

public class GetClassesByUserIdQueryHandler : IRequestHandler<GetClassesByUserIdQuery, Result<IEnumerable<GetSimpleClassResponse>>>
{
    private readonly SchoolDbContext _semesterDbContext;
    private readonly IMapper _mapper;

    public GetClassesByUserIdQueryHandler(SchoolDbContext semesterDbContext)
    {
        _semesterDbContext = semesterDbContext;
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Domain.Entities.Class, GetSimpleClassResponse>();
        }).CreateMapper();
    }


    async Task<Result<IEnumerable<GetSimpleClassResponse>>> IRequestHandler<GetClassesByUserIdQuery,
        Result<IEnumerable<GetSimpleClassResponse>>>.Handle(GetClassesByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var getClassesResponse = await _semesterDbContext.Classes
                .AsNoTracking()
                .Where(s => s.Students.Any(st => st.Id == request.UserId))
                .ProjectTo<GetSimpleClassResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return Result<IEnumerable<GetSimpleClassResponse>>.Create("Klasser tilhørende bruger fundet", getClassesResponse,
                ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<GetSimpleClassResponse>>.Create(e.Message, [],
                ResultStatus.Error);
        }
    }
        
}