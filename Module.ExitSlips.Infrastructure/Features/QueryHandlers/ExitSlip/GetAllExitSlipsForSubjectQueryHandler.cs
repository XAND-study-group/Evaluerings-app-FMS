﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.ExitSlip.Application.Features.ExitSlip.Query;
using Module.ExitSlip.Infrastructure.DbContexts;
using SharedKernel.Dto.Features.Evaluering.ExitSlip.Query;
using SharedKernel.Models;

namespace Module.ExitSlip.Infrastructure.Features.QueryHandlers.ExitSlip;

public class GetAllExitSlipsForSubjectQueryHandler : IRequestHandler<GetAllExitSlipsforSubjectQuery,
    Result<IEnumerable<GetSimpleExitSlipsResponse?>>>
{
    private readonly ExitSlipDbContext _exitSlipDbContext;
    private readonly IMapper _mapper;

    public GetAllExitSlipsForSubjectQueryHandler(ExitSlipDbContext exitSlipDbContext, IMapper mapper)
    {
        _exitSlipDbContext = exitSlipDbContext;
        _mapper = mapper;
    }

    async Task<Result<IEnumerable<GetSimpleExitSlipsResponse?>>>
        IRequestHandler<GetAllExitSlipsforSubjectQuery, Result<IEnumerable<GetSimpleExitSlipsResponse?>>>.Handle(
            GetAllExitSlipsforSubjectQuery request,
            CancellationToken cancellationToken)
    {
        try
        {
            var exitSlips = await _exitSlipDbContext.ExitSlips
                .AsNoTracking()
                .Where(e => e.SubjectId == request.subjectId)
                .ToArrayAsync(cancellationToken);
            
            var response = _mapper.Map<IEnumerable<GetSimpleExitSlipsResponse?>>(exitSlips);

            return Result<IEnumerable<GetSimpleExitSlipsResponse?>>.Create("ExitSlip fundet", response,
                ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<GetSimpleExitSlipsResponse?>>.Create(e.Message, [], ResultStatus.Error);
        }
    }
}