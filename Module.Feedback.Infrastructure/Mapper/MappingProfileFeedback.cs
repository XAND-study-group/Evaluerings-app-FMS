using AutoMapper;
using Module.Feedback.Domain;
using SharedKernel.Dto.Features.Evaluering.Feedback.Query;
using SharedKernel.Dto.Features.Evaluering.Room.Query;
using GetCommentResponse = SharedKernel.Dto.Features.Evaluering.Feedback.Query.GetCommentResponse;
using GetVoteResponse = SharedKernel.Dto.Features.Evaluering.Feedback.Query.GetVoteResponse;

namespace Module.Feedback.Infrastructure.Mapper
{
    public class MappingProfileFeedback:Profile
    {
        public MappingProfileFeedback()
        {
            CreateMap<Domain.Feedback, GetAllFeedbacksResponse>()
                .ForMember(dest => dest.HashedId, opt => opt.MapFrom(src => src.HashedUserId.Value))
                .ForMember(dest => dest.Problem, opt => opt.MapFrom(src => src.Problem.Value))
                .ForMember(dest => dest.Solution, opt => opt.MapFrom(src => src.Solution.Value));

            CreateMap<Comment, GetCommentResponse>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.CommentText, opt => opt.MapFrom(src => src.CommentText.Value));


            CreateMap<Vote, GetVoteResponse>()
                .ForMember(dest => dest.HashedId, opt => opt.MapFrom(src => src.HashedUserId.Value));


            CreateMap<Room, GetAllRoomsResponse>();
            CreateMap<Room, GetCommentResponse>();
            CreateMap<Room, GetFeedbackResponse>();
            CreateMap<Room, GetRoomResponse>();
            CreateMap<Room, GetVoteResponse>();
        }

    }
}
