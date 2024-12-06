using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.ExitSlip.Application.Features.ExitSlip.Query;
using Module.ExitSlip.Infrastructure.DbContexts;
using SharedKernel.Dto.Features.Evaluering.ExitSlip.Query;
using SharedKernel.Models;

namespace Module.ExitSlip.Infrastructure.Features.QueryHandlers.ExitSlip
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
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);

                return Result<GetDetailedExitSlipResponse?>.Create("ExitSLip funder", response, ResultStatus.Success);
            }
            catch (Exception e)
            {

                return Result<GetDetailedExitSlipResponse?>.Create(e.Message, null, ResultStatus.Error);
            }
        }
    }
}
