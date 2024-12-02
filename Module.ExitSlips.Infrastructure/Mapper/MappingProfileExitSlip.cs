using AutoMapper;
using Module.ExitSlip.Domain.Entities;
using SharedKernel.Dto.Features.Evaluering.Answer.Command;
using SharedKernel.Dto.Features.Evaluering.Answer.Query;
using SharedKernel.Dto.Features.Evaluering.ExitSlip.Command;
using SharedKernel.Dto.Features.Evaluering.ExitSlip.Query;
using SharedKernel.Dto.Features.Evaluering.Question.Command;
using SharedKernel.Dto.Features.Evaluering.Question.Query;

namespace Module.ExitSlip.Infrastructure.Mapper
{
    public class MappingProfileExitSlip : Profile
    {
        public MappingProfileExitSlip()
        {
            CreateMap<Answer, GetSimpleAnswerResponse>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text.Value)); 
            CreateMap<Answer, GetDetailedAnswerResponse>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text.Value));

            CreateMap<Question, GetSimpleQuestionsResponse>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text.Value));
            CreateMap<Question, GetDetailedQuestionsResponse>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text.Value));

            CreateMap<Domain.Entities.ExitSlip, GetSimpleExitSlipsResponse>();
            CreateMap<Domain.Entities.ExitSlip, GetExitSlipsWithAnswersResponse>();
            CreateMap<Domain.Entities.ExitSlip, GetDetailedExitSlipResponse>();
        }
    }
}