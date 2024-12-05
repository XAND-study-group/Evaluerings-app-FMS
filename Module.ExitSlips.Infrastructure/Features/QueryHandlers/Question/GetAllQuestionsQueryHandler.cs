using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.ExitSlip.Application.Abstractions;
using Module.ExitSlip.Application.Features.Question.Query;
using SharedKernel.Dto.Features.Evaluering.Question.Query;
using SharedKernel.Models;

namespace Module.ExitSlip.Infrastructure.Features.QueryHandlers.Question
{
    public class GetAllQuestionsQueryHandler : IRequestHandler<GetAllQuestionsQuery, Result<IEnumerable<GetSimpleQuestionsResponse>>>
    {
        private readonly IExitSlipDbContext _exitSlipDbContext;
        private readonly IMapper _mapper;

        public GetAllQuestionsQueryHandler(IExitSlipDbContext exitSlipDbContext, IMapper mapper)
        {
            _exitSlipDbContext = exitSlipDbContext;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<GetSimpleQuestionsResponse>>> Handle(GetAllQuestionsQuery query, CancellationToken cancellationToken)
        {
            var questions= await _exitSlipDbContext.Questions
                .AsNoTracking()
                .ProjectTo<GetSimpleQuestionsResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            
            return Result<IEnumerable<GetSimpleQuestionsResponse>>.Create("Success", questions, ResultStatus.Success);
        }
    }
}
