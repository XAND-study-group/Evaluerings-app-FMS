using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Module.ExitSlip.Domain.Entities;
using Module.ExitSlip.Domain.Test.Fakes;
using Module.ExitSlip.Domain.ValueObjects;
using Module.ExitSlip.Infrastructure.Mapper;
using SharedKernel.Dto.Features.Evaluering.Answer.Query;
using SharedKernel.Dto.Features.Evaluering.ExitSlip.Query;
using SharedKernel.Dto.Features.Evaluering.Question.Query;
using SharedKernel.Enums.Features.Evaluering.ExitSlip;

namespace Module.ExitSlip.Domain.Test
{
    public class ExitSlipAutoMapperConfigurationIsValidTests
    {
        private readonly IMapper _mapper;

        public ExitSlipAutoMapperConfigurationIsValidTests()
        {
            var config = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfileExitSlip>(); });

            _mapper = config.CreateMapper();
        }
        #region Answer Mapping Tests
        [Theory]
        [InlineData("Test Answer", "Test Question")]
        public void ShouldMapAnswerToGetSimpleAnswerResponse(string answerText, string questionText)
        {
            // Arrange
            var exitSlip = FakeExitSlip.Create(Guid.NewGuid(), Guid.NewGuid(), "Test ExitSlip", new MaxQuestionCount(5), ExitSlipActiveStatus.Active);
            var question = exitSlip.AddQuestion(questionText, Guid.NewGuid());

            var source = question.AddAnswer(answerText, Guid.NewGuid());

            // Act
            var destination = _mapper.Map<GetSimpleAnswerResponse>(source);

            // Assert
            Assert.Equal(source.Text.Value, destination.Text);
            Assert.Equal(source.UserId, destination.UserId);
        }
        [Theory]
        [InlineData("Test Answer", "Test Question")]
        public void ShouldMapAnswerToGetDetailedAnswerResponse(string answerText, string questionText)
        {
            // Arrange
            var exitSlip = FakeExitSlip.Create(Guid.NewGuid(), Guid.NewGuid(), "Test ExitSlip", new MaxQuestionCount(5), ExitSlipActiveStatus.Active);
            var question = exitSlip.AddQuestion(questionText, Guid.NewGuid());

            var source = question.AddAnswer(answerText, Guid.NewGuid());

            // Act
            var destination = _mapper.Map<GetDetailedAnswerResponse>(source);

            // Assert
            Assert.Equal(source.Text.Value, destination.Text);
            Assert.Equal(question.Id, destination.QuestionId);
        }
        #endregion   

        #region Question Mapping Tests
        [Theory]
        [InlineData("Test Question")]
        public void ShouldMapQuestionToGetSimpleQuestionsResponse(string questionText)
        {
            // Arrange
            var exitSlip = Entities.ExitSlip.Create(Guid.NewGuid(), Guid.NewGuid(), "Test ExitSlip", new MaxQuestionCount(5), ExitSlipActiveStatus.Active);
            var source = exitSlip.AddQuestion(questionText, Guid.NewGuid());

            // Act
            var destination = _mapper.Map<GetSimpleQuestionsResponse>(source);

            // Assert
            Assert.Equal(source.Id, destination.QuestionId);
            Assert.Equal(source.Text.Value, destination.Text);
            Assert.Equal(exitSlip.Id, destination.ExitSlipId);
            Assert.Equal(source.Answers.Count, destination.Answers.Count());
        }
        [Theory]
        [InlineData("Test Question")]
        public void ShouldMapQuestionToGetDetailsQuestionsResponse(string questionText)
        {
            // Arrange
            var exitSlip = Entities.ExitSlip.Create(Guid.NewGuid(), Guid.NewGuid(), "Test ExitSlip", new MaxQuestionCount(5), ExitSlipActiveStatus.Active);
            var source = exitSlip.AddQuestion(questionText, Guid.NewGuid());

            // Act
            var destination = _mapper.Map<GetDetailedQuestionsResponse>(source);

            // Assert
            Assert.Equal(source.Id, destination.QuestionId);
            Assert.Equal(source.Text.Value, destination.Text);
            Assert.Equal(exitSlip.Id, destination.ExitSlipId);
            Assert.Equal(source.Answers.Count, destination.Answers.Count());
        }
        #endregion

        #region ExitSlip Mapping Tests
        [Theory]
        [InlineData("Test Title")]
        public void ShouldMapExitSlipToGetSimpleExitSlipsResponse(string title)
        {
            // Arrange
            var source = Entities.ExitSlip.Create(Guid.NewGuid(), Guid.NewGuid(), title, new MaxQuestionCount(5), ExitSlipActiveStatus.Active);
            source.AddQuestion("Test Question", Guid.NewGuid());

            // Act
            var destination = _mapper.Map<GetSimpleExitSlipsResponse>(source);

            // Assert
            Assert.Equal(source.LectureId, destination.LectureId);
            Assert.Equal(source.SubjectId, destination.SubjectId);
            Assert.Equal(source.Title.Value, destination.Title);
            Assert.Equal(source.MaxQuestionCount.Value, destination.MaxQuestionCount);
            Assert.Equal(source.ActiveStatus, destination.ActiveStatus);
        }

        [Theory]
        [InlineData("Test Title2")]
        public void ShouldMapExitSlipToGetDetailsExitSlipResponse(string title)
        {
            // Arrange
            var source = Entities.ExitSlip.Create(Guid.NewGuid(), Guid.NewGuid(), title, new MaxQuestionCount(5), ExitSlipActiveStatus.Active);

            // Act
            var destination = _mapper.Map<GetDetailedExitSlipResponse>(source);

            // Assert
            Assert.Equal(source.LectureId, destination.LectureId);
            Assert.Equal(source.SubjectId, destination.SubjectId);
            Assert.Equal(source.Title.Value, destination.Title);
            Assert.Equal(source.MaxQuestionCount.Value, destination.MaxQuestionCount);
            Assert.Equal(source.ActiveStatus, destination.ActiveStatus);
        }

        [Theory]
        [InlineData("Test Title3", "AnswerTest")]
        public void ShouldMapExitSlipToGetExitSlipsWithAnswersResponse(string title, string answer)
        {
            // Arrange
            var source = Entities.ExitSlip.Create(Guid.NewGuid(), Guid.NewGuid(), title, new MaxQuestionCount(5), ExitSlipActiveStatus.Active);

            // Act
            var destination = _mapper.Map<GetExitSlipsWithAnswersResponse>(source);

            // Assert
            Assert.Equal(source.LectureId, destination.LectureId);
            Assert.Equal(source.SubjectId, destination.SubjectId);
            Assert.Equal(source.Title.Value, destination.Title);
            Assert.Equal(source.ActiveStatus, destination.ActiveStatus);
        }

        #endregion
    }
}