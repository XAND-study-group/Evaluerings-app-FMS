using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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
        private IExitSlipDbContext _exitSlipDbContext;
        private readonly AutoMapper.IMapper _mapper;

        public GetAllAnswersForQuestionIdQueryHandler(IExitSlipDbContext exitSlipDbContext, AutoMapper.IMapper mapper)
        {
            _exitSlipDbContext = exitSlipDbContext;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<GetSimpleAnswerResponse>>> Handle(GetAllAnswersForQuestionIdQuery query, CancellationToken cancellationToken)
        {
            var answers = (await _exitSlipDbContext.Questions
                .Include(q => q.Answers)
                .SingleAsync(q => q.Id == query.QuestionId)).Answers;
            if (answers == null)
                return Result<IEnumerable<GetSimpleAnswerResponse>>.Create("Ingen svar blev fundet udfra det gældende spørgsmål", null, ResultStatus.Error);

            var response = _mapper.Map<IEnumerable<GetSimpleAnswerResponse>>(answers);
            return Result<IEnumerable<GetSimpleAnswerResponse>>.Create("Success", response, ResultStatus.Success);
        }
    }
}
