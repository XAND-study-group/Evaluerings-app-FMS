using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Module.ExitSlip.Application.Abstractions;
using Module.ExitSlip.Application.Features.Answer.Query;
using SharedKernel.Dto.Features.Evaluering.Answer.Query;
using SharedKernel.Models;

namespace Module.ExitSlip.Infrastructure.Features.QueryHandlers.Answer
{
    public class GetAllAnswersForQuestionIdQueryHandler : IRequestHandler<GetAllAnswersForQuestionIdQuery, Result<IEnumerable<GetSimpleAnswerResponse>>>
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IMapper _mapper;

        public GetAllAnswersForQuestionIdQueryHandler(IAnswerRepository answerRepository, IMapper mapper)
        {
            _answerRepository = answerRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<GetSimpleAnswerResponse>>> Handle(GetAllAnswersForQuestionIdQuery query, CancellationToken cancellationToken)
        {
            var answers = await _answerRepository.Ge(query.QuestionId);
            if (answers == null)
            {
                return Result<IEnumerable<GetSimpleAnswerResponse>>.Failure("No answers found for the given question ID");
            }

            var response = _mapper.Map<IEnumerable<GetSimpleAnswerResponse>>(answers);
            return Result<IEnumerable<GetSimpleAnswerResponse>>.Success(response);
        }
    }
}
