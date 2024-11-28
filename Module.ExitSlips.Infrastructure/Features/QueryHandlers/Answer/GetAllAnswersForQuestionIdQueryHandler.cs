using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.ExitSlip.Application.Abstractions;
using Module.ExitSlip.Application.Features.Answer.Query;
using Module.ExitSlip.Infrastructure.DbContexts;
using SharedKernel.Dto.Features.Evaluering.Answer.Query;
using SharedKernel.Models;

namespace Module.ExitSlip.Infrastructure.Features.QueryHandlers.Answer
{
    public class GetAllAnswersForQuestionIdQueryHandler : IRequestHandler<GetAllAnswersForQuestionIdQuery, Result<IEnumerable<GetSimpleAnswerResponse>>>
    {
        private readonly IExitSlipDbContext _exitSlipDbContext;
        private readonly AutoMapper.IMapper _mapper;

        public GetAllAnswersForQuestionIdQueryHandler(IExitSlipDbContext exitSlipDbContext, AutoMapper.IMapper mapper)
        {
            _exitSlipDbContext = exitSlipDbContext;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<GetSimpleAnswerResponse>>> Handle(GetAllAnswersForQuestionIdQuery query, CancellationToken cancellationToken)
        {
            var answers = await _exitSlipDbContext.Questions
                .Include(q => q.Answers)
                .Where(q => q.Id == query.QuestionId)
                .Select(a=>a.Answers)
                .ProjectTo<GetSimpleAnswerResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            if (answers == null)
                return Result<IEnumerable<GetSimpleAnswerResponse>>.Create("Ingen svar blev fundet udfra det gældende spørgsmål", null, ResultStatus.Error);

            return Result<IEnumerable<GetSimpleAnswerResponse>>.Create("Success", answers, ResultStatus.Success);
        }
    }
}
