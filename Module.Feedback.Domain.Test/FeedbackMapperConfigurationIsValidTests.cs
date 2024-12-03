using AutoMapper;
using Module.Feedback.Domain;
using Module.Feedback.Domain.DomainServices.Interfaces;
using Module.Feedback.Infrastructure.Mapper;
using SharedKernel.Dto.Features.Evaluering.Feedback.Query;
using SharedKernel.Dto.Features.Evaluering.Proxy;
using SharedKernel.Dto.Features.Evaluering.Room.Query;
using SharedKernel.Enums.Features.Evaluering.Feedback;
using SharedKernel.Enums.Features.Vote;
using SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Module.Feedback.Domain.Test.Fakes;
using Xunit;

namespace Module.Feedback.Domain.Test
{
    public class FeedbackAutoMapperConfigurationIsValidTests
    {
        private readonly IMapper _mapper;
        private readonly IValidationServiceProxy _validationServiceProxy = new MockValidationServiceProxy();

        public FeedbackAutoMapperConfigurationIsValidTests()
        {
            var config = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfileFeedback>(); });
            _mapper = config.CreateMapper();
        }

        #region Feedback Mappings

        [Theory]
        [InlineData("Test Title", "Test Problem", "Test Solution", "Test Room")]
        public async Task ShouldMapFeedbackToGetAllFeedbacksResponse(string title, string problem, string solution, string roomTitle)
        {
            // Arrange
            var room = FakeRoom.Create(roomTitle, "Test Description");
            var source = await FakeFeedback.CreateAsync(Guid.NewGuid(), title, problem, solution, room, _validationServiceProxy);

            // Act
            var destination = _mapper.Map<GetAllFeedbacksResponse>(source);

            // Assert
            Assert.Equal(source.Id, destination.Id);
            Assert.Equal(source.HashedUserId, destination.HashedId);
            Assert.Equal(source.Title, destination.Title);
            Assert.Equal(source.Problem, destination.Problem);
            Assert.Equal(source.Solution, destination.Solution);
        }

        [Theory]
        [InlineData("Test Title", "Test Problem", "Test Solution", "Test Room", "Test Comment")]
        public async Task ShouldMapFeedbackToGetCommentResponse(string title, string problem, string solution, string roomTitle, string commentText)
        {
            // Arrange
            var room = FakeRoom.Create(roomTitle, "Test Description");
            var feedback = await FakeFeedback.CreateAsync(Guid.NewGuid(), title, problem, solution, room, _validationServiceProxy);
            var source = await feedback.AddComment(Guid.NewGuid(), commentText, _validationServiceProxy);

            // Act
            var destination = _mapper.Map<SharedKernel.Dto.Features.Evaluering.Feedback.Query.GetCommentResponse>(source);

            // Assert
            Assert.Equal(source.Id, destination.Id);
            Assert.Equal(source.UserId, destination.UserId);
            Assert.Equal(source.CommentText, destination.CommentText);
        }


        [Theory]
        [InlineData("Test Title", "Test Problem", "Test Solution", "Test Room", VoteScale.UpVote)]
        public async Task ShouldMapFeedbackToGetVoteResponse(string title, string problem, string solution, string roomTitle, VoteScale voteScale)
        {
            // Arrange
            var room = FakeRoom.Create(roomTitle, "Test Description");
            var feedback = await FakeFeedback.CreateAsync(Guid.NewGuid(), title, problem, solution, room, _validationServiceProxy);
            var source = feedback.AddVote(Guid.NewGuid(), voteScale);

            // Act
            var destination = _mapper.Map<SharedKernel.Dto.Features.Evaluering.Feedback.Query.GetVoteResponse>(source);

            // Assert
            Assert.Equal(source.Id, destination.Id);
            Assert.Equal(source.HashedUserId, destination.HashedId);
            Assert.Equal(source.VoteScale, destination.VoteScale);
        }

        #endregion

        #region Room Mappings

        [Theory]
        [InlineData("Test Room", "Test Description")]
        public void ShouldMapRoomToGetAllRoomsResponse(string title, string description)
        {
            // Arrange
            var source = FakeRoom.Create(title, description);

            // Act
            var destination = _mapper.Map<GetAllRoomsResponse>(source);

            // Assert
            Assert.Equal(source.Id, destination.RoomId);
            Assert.Equal(source.Title, destination.Title);
            Assert.Equal(source.Description, destination.Description);
        }

        [Theory]
        [InlineData("Test Title", "Test Problem", "Test Solution", "Test Room", "Test Comment")]
        public async Task ShouldMapRoomToGetCommentResponse(string title, string problem, string solution, string roomTitle, string commentText)
        {
            // Arrange
            var room = FakeRoom.Create(roomTitle, "Test Description");
            var feedback = await FakeFeedback.CreateAsync(Guid.NewGuid(), title, problem, solution, room, _validationServiceProxy); 
            var source = await feedback.AddComment(Guid.NewGuid(), commentText, _validationServiceProxy); 

            // Act
            var destination = _mapper.Map<SharedKernel.Dto.Features.Evaluering.Feedback.Query.GetCommentResponse>(source); 

            // Assert
            Assert.Equal(source.Id, destination.Id);
            Assert.Equal(source.CommentText, destination.CommentText);
        }

        [Theory]
        [InlineData("Test Room", "Test Description", "Test Title", "Test Problem", "Test Solution")]
        public async Task ShouldMapRoomToGetFeedbackResponse(string title, string description, string feedbackTitle, string problem, string solution)
        {
            // Arrange
            var room = FakeRoom.Create(title, description);
            var source = await FakeFeedback.CreateAsync(Guid.NewGuid(), feedbackTitle, problem, solution, room, _validationServiceProxy); 
            room.AddFeedback(source);

            // Act
            var destination = _mapper.Map<GetFeedbackResponse>(source); 

            // Assert
            Assert.Equal(source.Id, destination.FeedbackId);
            Assert.Equal(source.HashedUserId, destination.HashedId);
            Assert.Equal(source.Problem, destination.Problem);
            Assert.Equal(source.Solution, destination.Solution);
        }

        [Theory]
        [InlineData("Test Room", "Test Description")]
        public void ShouldMapRoomToGetRoomResponse(string title, string description)
        {
            // Arrange
            var source = FakeRoom.Create(title, description);

            // Act
            var destination = _mapper.Map<GetRoomResponse>(source);

            // Assert
            Assert.Equal(source.Id, destination.RoomId);
            Assert.Equal(source.Title, destination.Title);
            Assert.Equal(source.Description, destination.Description);
        }

        //[Theory]
        //[InlineData("Test Room", "Test Description", "Test Title", "Test Problem", "Test Solution", VoteScale.UpVote)]
        //public async Task ShouldMapRoomToGetVoteResponse(string roomTitle, string roomDescription, string feedbackTitle, string problem, string solution, VoteScale voteScale)
        //{
        //    // Arrange
        //    var room = FakeRoom.Create(roomTitle, roomDescription);
        //    var source = await FakeFeedback.CreateAsync(Guid.NewGuid(), feedbackTitle, problem, solution, room, _validationServiceProxy);
        //    room.AddFeedback(source);
        //    var vote = source.AddVote(Guid.NewGuid(), voteScale);

        //    // Act
        //    var destination = _mapper.Map<SharedKernel.Dto.Features.Evaluering.Room.Query.GetVoteResponse>(source);

        //    // Assert
        //    Assert.Equal(vote.Id, destination.Id);
        //    Assert.Equal(vote.VoteScale, destination.VoteScale);
        //}

        #endregion
    }

    public class MockValidationServiceProxy : IValidationServiceProxy
    {
        public Task<GeminiResponse> IsAcceptableCommentAsync(string commentText)
        {
            return Task.FromResult(new GeminiResponse(true, ""));
        }

        public Task<GeminiResponse> IsAcceptableContentAsync(string title, string problem, string solution)
        {
            return Task.FromResult(new GeminiResponse(true, ""));
        }
    }
}