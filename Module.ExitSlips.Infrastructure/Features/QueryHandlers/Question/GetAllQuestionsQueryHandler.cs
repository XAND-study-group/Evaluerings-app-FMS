using AutoMapper;
using MediatR;
using Module.ExitSlip.Application.Abstractions;
using Module.ExitSlip.Application.Features.Question.Query;
using SharedKernel.Dto.Features.Evaluering.Question.Query;
using SharedKernel.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Dto.Features.School.Lecture.Query;

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
                .ProjectTo<IEnumerable<GetSimpleQuestionsResponse>>(_mapper.ConfigurationProvider)
                .SingleAsync(cancellationToken);

            if(questions is null)
                return Result<IEnumerable<GetSimpleQuestionsResponse>>.Create("Ingen spørgsmål blev fundet", null, ResultStatus.Error);
            
            return Result<IEnumerable<GetSimpleQuestionsResponse>>.Create("Success", questions, ResultStatus.Success);
        }
    }
}
