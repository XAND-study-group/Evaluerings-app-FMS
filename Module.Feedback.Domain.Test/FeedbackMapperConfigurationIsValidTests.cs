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
            var room = Room.Create(roomTitle, "Test Description");
            var feedback = await Feedback.CreateAsync(Guid.NewGuid(), title, problem, solution, room, _validationServiceProxy);

            // Act
            var result = _mapper.Map<GetAllFeedbacksResponse>(feedback);

            // Assert
            Assert.Equal(feedback.Id, result.Id);
            Assert.Equal(feedback.HashedUserId, result.HashedId);
            Assert.Equal(feedback.Title, result.Title);
            Assert.Equal(feedback.Problem, result.Problem);
            Assert.Equal(feedback.Solution, result.Solution);
        }

        [Theory]
        [InlineData("Test Title", "Test Problem", "Test Solution", "Test Room", "Test Comment")]
        public async Task ShouldMapFeedbackToGetCommentResponse(string title, string problem, string solution, string roomTitle, string commentText)
        {
            // Arrange
            var room = Room.Create(roomTitle, "Test Description");
            var feedback = await Feedback.CreateAsync(Guid.NewGuid(), title, problem, solution, room, _validationServiceProxy);
            var comment = await feedback.AddComment(Guid.NewGuid(), commentText, _validationServiceProxy);

            // Act
            var result = _mapper.Map<SharedKernel.Dto.Features.Evaluering.Feedback.Query.GetCommentResponse>(comment);

            // Assert
            Assert.Equal(comment.Id, result.Id);
            Assert.Equal(comment.UserId, result.UserId);
            Assert.Equal(comment.CommentText, result.CommentText);
        }


        [Theory]
        [InlineData("Test Title", "Test Problem", "Test Solution", "Test Room", VoteScale.UpVote)]
        public async Task ShouldMapFeedbackToGetVoteResponse(string title, string problem, string solution, string roomTitle, VoteScale voteScale)
        {
            // Arrange
            var room = Room.Create(roomTitle, "Test Description");
            var feedback = await Feedback.CreateAsync(Guid.NewGuid(), title, problem, solution, room, _validationServiceProxy);
            var vote = feedback.AddVote(Guid.NewGuid(), voteScale);

            // Act
            var result = _mapper.Map<SharedKernel.Dto.Features.Evaluering.Feedback.Query.GetVoteResponse>(vote);

            // Assert
            Assert.Equal(vote.Id, result.Id);
            Assert.Equal(vote.HashedUserId, result.HashedId);
            Assert.Equal(vote.VoteScale, result.VoteScale);
        }

        #endregion

        #region Room Mappings

        [Theory]
        [InlineData("Test Room", "Test Description")]
        public void ShouldMapRoomToGetAllRoomsResponse(string title, string description)
        {
            // Arrange
            var room = Room.Create(title, description);

            // Act
            var result = _mapper.Map<GetAllRoomsResponse>(room);

            // Assert
            Assert.Equal(room.Id, result.RoomId);
            Assert.Equal(room.Title, result.Title);
            Assert.Equal(room.Description, result.Description);
        }

        [Theory]
        [InlineData("Test Title", "Test Problem", "Test Solution", "Test Room", "Test Comment")]
        public async Task ShouldMapRoomToGetCommentResponse(string title, string problem, string solution, string roomTitle, string commentText)
        {
            // Arrange
            var room = Room.Create(roomTitle, "Test Description");
            var feedback = await Feedback.CreateAsync(Guid.NewGuid(), title, problem, solution, room, _validationServiceProxy); 
            var comment = await feedback.AddComment(Guid.NewGuid(), commentText, _validationServiceProxy); 

            // Act
            var result = _mapper.Map<SharedKernel.Dto.Features.Evaluering.Room.Query.GetCommentResponse>(comment); 

            // Assert
            Assert.Equal(comment.Id, result.Id);
            Assert.Equal(comment.CommentText, result.CommentText);
        }

        [Theory]
        [InlineData("Test Room", "Test Description", "Test Title", "Test Problem", "Test Solution")]
        public async Task ShouldMapRoomToGetFeedbackResponse(string title, string description, string feedbackTitle, string problem, string solution)
        {
            // Arrange
            var room = Room.Create(title, description);
            var feedback = await Feedback.CreateAsync(Guid.NewGuid(), feedbackTitle, problem, solution, room, _validationServiceProxy); 
            room.AddFeedback(feedback);

            // Act
            var result = _mapper.Map<GetFeedbackResponse>(feedback); 

            // Assert
            Assert.Equal(feedback.Id, result.FeedbackId);
            Assert.Equal(feedback.HashedUserId, result.HashedId);
            Assert.Equal(feedback.Problem, result.Problem);
            Assert.Equal(feedback.Solution, result.Solution);
        }

        [Theory]
        [InlineData("Test Room", "Test Description")]
        public void ShouldMapRoomToGetRoomResponse(string title, string description)
        {
            // Arrange
            var room = Room.Create(title, description);

            // Act
            var result = _mapper.Map<GetRoomResponse>(room);

            // Assert
            Assert.Equal(room.Id, result.RoomId);
            Assert.Equal(room.Title, result.Title);
            Assert.Equal(room.Description, result.Description);
        }

        //[Theory]
        //[InlineData("Test Room", "Test Description", "Test Title", "Test Problem", "Test Solution", VoteScale.UpVote)]
        //public async Task ShouldMapRoomToGetVoteResponse(string roomTitle, string roomDescription, string feedbackTitle, string problem, string solution, VoteScale voteScale)
        //{
        //    // Arrange
        //    var room = Room.Create(roomTitle, roomDescription);
        //    var feedback = await Feedback.CreateAsync(Guid.NewGuid(), feedbackTitle, problem, solution, room, _validationServiceProxy);
        //    room.AddFeedback(feedback); 
        //    var vote = feedback.AddVote(Guid.NewGuid(), voteScale); // Add vote to Feedback

        //    // Act
        //    var result = _mapper.Map<SharedKernel.Dto.Features.Evaluering.Room.Query.GetVoteResponse>(feedback); 

        //    // Assert
        //    Assert.Equal(vote.Id, result.Id);
        //    Assert.Equal(vote.VoteScale, result.VoteScale);
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