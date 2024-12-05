using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.ExitSlip.Application.Abstractions;
using Module.ExitSlip.Application.Features.ExitSlip.Query;
using SharedKernel.Dto.Features.Evaluering.ExitSlip.Query;
using SharedKernel.Dto.Features.Evaluering.Question.Query;
using SharedKernel.Models;

namespace Module.ExitSlip.Infrastructure.Features.QueryHandlers.ExitSlip
{
    public class GetExitSlipWithQuestionsQueryHandler : IRequestHandler<GetExitSlipWithQuestionsQuery,
        Result<IEnumerable<GetDetailsExitSlipResponse>?>>
    {
        // TODO: Når automapperen er sat op, skal disse flyttes i primary Ctor. 
        private readonly IExitSlipDbContext _exitSlipDbContext;
        private readonly IMapper _mapper;
        public GetExitSlipWithQuestionsQueryHandler(IExitSlipDbContext exitSlipDbContext)
        {
            _exitSlipDbContext = exitSlipDbContext;
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Domain.Entities.ExitSlip, GetDetailsExitSlipResponse>();
                cfg.CreateMap<Domain.Entities.Question, GetSimpleQuestionsResponse>();
            }).CreateMapper();
        }

        async Task<Result<IEnumerable<GetDetailsExitSlipResponse>?>> IRequestHandler<GetExitSlipWithQuestionsQuery, Result<IEnumerable<GetDetailsExitSlipResponse>?>>
             .Handle(GetExitSlipWithQuestionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _exitSlipDbContext.ExitSlips
                    .Where(e => e.Id == request.exitSlipId)
                    .Include(e => e.Questions)
                    .ProjectTo<GetDetailsExitSlipResponse>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return Result<IEnumerable<GetDetailsExitSlipResponse>?>.Create("ExitSlip med spørgsmål", response, ResultStatus.Success);
            }
            catch (Exception e)
            {
                return Result<IEnumerable<GetDetailsExitSlipResponse>?>.Create(e.Message, [], ResultStatus.Error);
            }
        }
    }
}
