using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Module.Feedback.Domain;
using SharedKernel.Dto.Features.Evaluering.Feedback.Command;
using SharedKernel.Dto.Features.Evaluering.Feedback.Query;
using SharedKernel.Dto.Features.Evaluering.Room.Command;
using SharedKernel.Dto.Features.Evaluering.Room.Query;
using GetCommentResponse = SharedKernel.Dto.Features.Evaluering.Feedback.Query.GetCommentResponse;
using GetVoteResponse = SharedKernel.Dto.Features.Evaluering.Feedback.Query.GetVoteResponse;

namespace Module.Feedback.Infrastructure.IMapper
{
    public class MappingProfileFeedback:Profile
    {
        public MappingProfileFeedback()
        {
            CreateMap<Domain.Feedback, ChangeFeedbackStatusRequest>();
            CreateMap<Domain.Feedback, CreateFeedbackRequest>();
            CreateMap<Domain.Feedback, DeleteFeedbackRequest>();
            CreateMap<Domain.Feedback, GetAllFeedbacksResponse>();
            CreateMap<Domain.Feedback, GetCommentResponse>();
            CreateMap<Domain.Feedback, GetVoteResponse>();

            CreateMap<Room, AddClassToRoomRequest>();
            CreateMap<Room, CreateRoomRequest>();
            CreateMap<Room, DeleteRoomRequest>();
            CreateMap<Room, RemoveClassIdFromRoomRequest>();
            CreateMap<Room, SubscribeToRoomNotificationRequest>();
            CreateMap<Room, UnsubscribeToRoomNotificationRequest>();
            CreateMap<Room, UpdateRoomRequest>();
            CreateMap<Room, GetAllRoomsResponse>();
            CreateMap<Room, GetCommentResponse>();
            CreateMap<Room, GetFeedbackResponse>();
            CreateMap<Room, GetRoomResponse>();
            CreateMap<Room, GetVoteResponse>();
        }

    }
}
