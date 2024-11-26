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
    public class GetExitSlipByIdQueryHandler : IRequestHandler<GetExitSlipByIdQuery, Result<GetDetailsExitSlipResponse?>>
    {

        private readonly ExitSlipDbContext _exitSlipDbContext;
        private readonly IMapper _mapper;
        public GetExitSlipByIdQueryHandler(ExitSlipDbContext exitSlipDbContext)
        {
            _exitSlipDbContext = exitSlipDbContext;
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Domain.Entities.ExitSlip, GetDetailsExitSlipResponse>();
                cfg.CreateMap<Domain.Entities.Question, GetSimpleExitSlipsResponse>();
            }).CreateMapper();
        }

        async Task<Result<GetDetailsExitSlipResponse?>> IRequestHandler<GetExitSlipByIdQuery, Result<GetDetailsExitSlipResponse?>>.Handle(GetExitSlipByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _exitSlipDbContext.ExitSlips
                    .AsNoTracking()
                    .Include(e => e.Questions)
                    .Where(e => e.Id == request.id)
                    .ProjectTo<GetDetailsExitSlipResponse?>(_mapper.ConfigurationProvider)
                    .SingleAsync();

                return Result<GetDetailsExitSlipResponse?>.Create("ExitSLip funder", response, ResultStatus.Success);
            }
            catch (Exception e)
            {

                return Result<GetDetailsExitSlipResponse?>.Create(e.Message, null, ResultStatus.Error);
            }
        }
    }
}
