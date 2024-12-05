using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.ExitSlip.Application.Abstractions;
using Module.ExitSlip.Application.Features.ExitSlip.Query;
using SharedKernel.Dto.Features.Evaluering.Answer.Query;
using SharedKernel.Dto.Features.Evaluering.ExitSlip.Query;
using SharedKernel.Dto.Features.Evaluering.Question.Query;
using SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ExitSlip.Infrastructure.Features.QueryHandlers.ExitSlip
{
    public class GetExitSlipWithAllAnswersQueryHandler : 
        IRequestHandler<GetExitSlipWithAllAnswersQuery, Result<GetExitSlipWithAnswersResponse?>>
    {
        // TODO: Når automapperen er sat op, skal disse flyttes i primary Ctor. 
        private readonly IExitSlipDbContext _exitSlipDbContext;
        private readonly IMapper _mapper;
        public GetExitSlipWithAllAnswersQueryHandler(IExitSlipDbContext exitSlipDbContext)
        {
            _exitSlipDbContext = exitSlipDbContext;
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Domain.Entities.ExitSlip, GetExitSlipWithAnswersResponse>();
                cfg.CreateMap<Domain.Entities.Question, GetDetailsQuestionsResponse>();
                cfg.CreateMap<Domain.Entities.Answer, GetSimpleAnswerResponse>();
            }).CreateMapper();
        }


        async Task<Result<GetExitSlipWithAnswersResponse?>> IRequestHandler<GetExitSlipWithAllAnswersQuery, Result<GetExitSlipWithAnswersResponse?>>
            .Handle(GetExitSlipWithAllAnswersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _exitSlipDbContext.ExitSlips
                    .AsNoTracking()
                    .Where(e => e.Id == request.exitSlipId)
                    .Include(e => e.Questions)
                    .ThenInclude(q => q.Answers)
                    .ProjectTo<GetExitSlipWithAnswersResponse>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();

                return Result<GetExitSlipWithAnswersResponse?>.Create("ExitSlip er fundet", response, ResultStatus.Success);    
            }
            catch (Exception e)
            {
                return Result<GetExitSlipWithAnswersResponse?>.Create(e.Message, null, ResultStatus.Error);
            }
        }
    }
}
