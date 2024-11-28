using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Module.ExitSlip.Domain.Entities;
using SharedKernel.Dto.Features.Evaluering.Answer.Command;
using SharedKernel.Dto.Features.Evaluering.Answer.Query;
using SharedKernel.Dto.Features.Evaluering.ExitSlip.Command;
using SharedKernel.Dto.Features.Evaluering.ExitSlip.Query;
using SharedKernel.Dto.Features.Evaluering.Question.Command;
using SharedKernel.Dto.Features.Evaluering.Question.Query;

namespace Module.ExitSlip.Infrastructure.IMapper
{
    public class MappingProfileExitSlip : Profile
    {
        public MappingProfileExitSlip()
        {
            CreateMap<Answer, CreateAnswerRequest>();
            CreateMap<Answer, UpdateAnswerRequest>();
            CreateMap<Answer, GetSimpleAnswerResponse>();
            CreateMap<Answer, GetDetailsAnswerResponse>();

            CreateMap<Question, CreateQuestionRequest>();
            CreateMap<Question, DeleteQuestionRequest>();
            CreateMap<Question, UpdateQuestionRequest>();
            CreateMap<Question, GetSimpleQuestionsResponse>();
            CreateMap<Question, GetDetailsQuestionsResponse>();

            CreateMap<Domain.Entities.ExitSlip, CreateExitSlipRequest>();
            CreateMap<Domain.Entities.ExitSlip, DeleteExitSlipRequest>();
            CreateMap<Domain.Entities.ExitSlip, UpdateExitSlipActiveStatusRequest>();
            CreateMap<Domain.Entities.ExitSlip, UpdateExitSlipRequest>();

            CreateMap<Domain.Entities.ExitSlip, GetSimpleExitSlipsResponse>();
            CreateMap<Domain.Entities.ExitSlip, GetExitSlipsWithAnswersResponse>();
            CreateMap<Domain.Entities.ExitSlip, GetDetailsExitSlipResponse>();
        }
    }
}