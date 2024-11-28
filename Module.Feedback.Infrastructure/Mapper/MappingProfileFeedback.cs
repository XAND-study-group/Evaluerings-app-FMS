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
            CreateMap<Domain.Feedback, GetAllFeedbacksResponse>();
            CreateMap<Domain.Feedback, GetCommentResponse>();
            CreateMap<Domain.Feedback, GetVoteResponse>();

            CreateMap<Room, GetAllRoomsResponse>();
            CreateMap<Room, GetCommentResponse>();
            CreateMap<Room, GetFeedbackResponse>();
            CreateMap<Room, GetRoomResponse>();
            CreateMap<Room, GetVoteResponse>();
        }

    }
}
