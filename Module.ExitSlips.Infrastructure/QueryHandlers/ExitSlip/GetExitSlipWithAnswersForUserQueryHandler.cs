using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.ExitSlip.Application.Features.ExitSlip.Query;
using Module.ExitSlip.Infrastructure.DbContexts;
using SharedKernel.Dto.Features.Evaluering.Answer.Query;
using SharedKernel.Dto.Features.Evaluering.ExitSlip.Query;
using SharedKernel.Dto.Features.Evaluering.Question.Query;
using SharedKernel.Models;

namespace Module.ExitSlip.Infrastructure.QueryHandlers.ExitSlip
{
    public class GetExitSlipWithAnswersForUserQueryHandler : IRequestHandler<GetExitSlipWithAnswersForUserQuery, Result<GetExitSlipsWithAnswersResponse>>
    {
        private readonly ExitSlipDbContext _exitSlipDbContext;
        private readonly IMapper _mapper;

        public GetExitSlipWithAnswersForUserQueryHandler(ExitSlipDbContext exitSlipDbContext)
        {
            _exitSlipDbContext = exitSlipDbContext;
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Domain.Entities.ExitSlip, GetExitSlipsWithAnswersResponse>();
                cfg.CreateMap<Domain.Entities.Question, GetDetailsQuestionsResponse>();
                cfg.CreateMap<Domain.Entities.Answer, GetSimpleAnswerResponse>();
            }).CreateMapper();
        }

        async Task<Result<GetExitSlipsWithAnswersResponse>> IRequestHandler<GetExitSlipWithAnswersForUserQuery, Result<GetExitSlipsWithAnswersResponse>>.Handle(GetExitSlipWithAnswersForUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _exitSlipDbContext.ExitSlips
                    .AsNoTracking()
                    .Where(e => e.Id == request.ExitSlipId)
                    .Include(e => e.Questions)
                    .ThenInclude(q => q.Answers.Where(a => a.UserId == request.userId))
                    .ProjectTo<GetExitSlipsWithAnswersResponse>(_mapper.ConfigurationProvider)
                    .SingleAsync();

                  return Result<GetExitSlipsWithAnswersResponse>.Create("ExitSLip er fundet", response, ResultStatus.Success);
            }
            catch (Exception e)
            {
                return Result<GetExitSlipsWithAnswersResponse>.Create(e.Message, null!, ResultStatus.Error);
            }


        }
    }
}
