﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.ExitSlip.Application.Features.ExitSlip.Query;
using Module.ExitSlip.Infrastructure.DbContexts;
using SharedKernel.Dto.Features.Evaluering.ExitSlip.Query;
using SharedKernel.Models;

namespace Module.ExitSlip.Infrastructure.Features.QueryHandlers.ExitSlip
{
    public class GetAllExitSlipsQueryHandler : IRequestHandler<GetAllExitSlipsQuery, Result<IEnumerable<GetSimpleExitSlipsResponse?>>>
    {
        private readonly ExitSlipDbContext _exitSlipDbContext;
        private readonly IMapper _mapper;

        public GetAllExitSlipsQueryHandler(ExitSlipDbContext exitSlipDbContext, IMapper mapper)
        {
            _exitSlipDbContext = exitSlipDbContext;
            _mapper = mapper;
        }

        async Task<Result<IEnumerable<GetSimpleExitSlipsResponse?>>> IRequestHandler<GetAllExitSlipsQuery, Result<IEnumerable<GetSimpleExitSlipsResponse?>>>.Handle(GetAllExitSlipsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _exitSlipDbContext.ExitSlips
                    .AsNoTracking()
                    .ProjectTo<GetSimpleExitSlipsResponse?>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return Result<IEnumerable<GetSimpleExitSlipsResponse?>>.Create("ExitSlip fundet", response, ResultStatus.Success);
            }
            catch (Exception e)
            {
                return Result<IEnumerable<GetSimpleExitSlipsResponse?>>.Create(e.Message, [], ResultStatus.Error);
            }
        }
    }
}