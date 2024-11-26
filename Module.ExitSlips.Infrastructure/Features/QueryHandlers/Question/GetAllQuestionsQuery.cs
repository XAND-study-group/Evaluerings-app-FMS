using AutoMapper;
using MediatR;
using Module.ExitSlip.Application.Abstractions;
using Module.ExitSlip.Application.Features.Question.Query;
using SharedKernel.Dto.Features.Evaluering.Question.Query;
using SharedKernel.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Module.ExitSlip.Infrastructure.Features.QueryHandlers.Question
{
    public class GetAllQuestionsQueryHandler : IRequestHandler<GetAllQuestionsQuery, Result<IEnumerable<GetSimpleQuestionsResponse>>>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public GetAllQuestionsQueryHandler(IQuestionRepository questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<GetSimpleQuestionsResponse>>> Handle(GetAllQuestionsQuery query, CancellationToken cancellationToken)
        {
            return Result<IEnumerable<GetSimpleQuestionsResponse>>.Create("response", [], ResultStatus.Error);
        }
    }
}
