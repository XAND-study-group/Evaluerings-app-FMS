using AutoMapper;
using MediatR;
using Module.ExitSlip.Application.Abstractions;
using Module.ExitSlip.Application.Features.Question.Query;
using SharedKernel.Dto.Features.Evaluering.Question.Query;
using SharedKernel.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
                .ToListAsync(cancellationToken);

            if(questions is null)
                return Result<IEnumerable<GetSimpleQuestionsResponse>>.Create("Ingen spørgsmål blev fundet", null, ResultStatus.Error);
            
            var response = _mapper.Map<IEnumerable<GetSimpleQuestionsResponse>>(questions);
            return Result<IEnumerable<GetSimpleQuestionsResponse>>.Create("Success", response, ResultStatus.Success);
        }
    }
}
