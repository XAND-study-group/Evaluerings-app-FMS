using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.ExitSlip.Application.Features.ExitSlip.Query;
using Module.ExitSlip.Infrastructure.DbContexts;
using SharedKernel.Dto.Features.Evaluering.ExitSlip.Query;
using SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ExitSlip.Infrastructure.QueryHandlers.ExitSlip
{
    public class GetExitSlipByIdQueryHandler : IRequestHandler<GetExitSlipByIdQuery, Result<GetDetailedExitSlipResponse?>>
    {

        private readonly ExitSlipDbContext _exitSlipDbContext;
        private readonly IMapper _mapper;
        public GetExitSlipByIdQueryHandler(ExitSlipDbContext exitSlipDbContext, IMapper mapper)
        {
            _exitSlipDbContext = exitSlipDbContext;
            _mapper = mapper;
        }

        async Task<Result<GetDetailedExitSlipResponse?>> IRequestHandler<GetExitSlipByIdQuery, Result<GetDetailedExitSlipResponse?>>.Handle(GetExitSlipByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _exitSlipDbContext.ExitSlips
                    .AsNoTracking()
                    .Include(e => e.Questions)
                    .Where(e => e.Id == request.id)
                    .ProjectTo<GetDetailedExitSlipResponse?>(_mapper.ConfigurationProvider)
                    .SingleAsync(cancellationToken);

                return Result<GetDetailedExitSlipResponse?>.Create("ExitSLip funder", response, ResultStatus.Success);
            }
            catch (Exception e)
            {

                return Result<GetDetailedExitSlipResponse?>.Create(e.Message, null, ResultStatus.Error);
            }
        }
    }
}
