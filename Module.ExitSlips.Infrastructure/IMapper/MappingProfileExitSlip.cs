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
            CreateMap<Answer, GetSimpleAnswerResponse>();
            CreateMap<Answer, GetDetailsAnswerResponse>();

            CreateMap<Question, GetSimpleQuestionsResponse>();
            CreateMap<Question, GetDetailsQuestionsResponse>();

            CreateMap<Domain.Entities.ExitSlip, GetSimpleExitSlipsResponse>();
            CreateMap<Domain.Entities.ExitSlip, GetExitSlipsWithAnswersResponse>();
            CreateMap<Domain.Entities.ExitSlip, GetDetailsExitSlipResponse>();
        }
    }
}