using Module.Feedback.Domain.DomainServices;
using Module.Feedback.Domain.Test.Fakes;
using Moq;
using SharedKernel.Dto.Features.Evaluering.Proxy;
using SharedKernel.Interfaces.DomainServices;

namespace Module.Feedback.Domain.Test;

public class CommentTests
{
    #region Tests

    #region Creational Tests

    [Theory]
    [MemberData(nameof(ValidCreationData))]
    public void Given_Valid_Comment_Data_Then_Create_Success(Guid userId, string commentText)
    {
        // Arrange
        var mockFeedbackAiService = new Mock<IValidationServiceProxy>();
        mockFeedbackAiService.Setup(f => f.IsAcceptableCommentAsync(commentText)).ReturnsAsync(new GeminiResponse(true,""));

        // Act
        var comment = Comment.CreateAsync(userId, commentText, mockFeedbackAiService.Object);

        // Assert
        Assert.NotNull(comment);
    }

    #endregion Creational Tests

    #region CommentText Tests

    [Fact]
    public void Given_Valid_CommentText_Then_Void()
    {
        // Arrange
        var comment = new FakeComment();
        var commentText = "This is a valid comment text";

        // Act
        comment.SetCommentText(commentText);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(" ")]
    [InlineData("")]
    public void Given_Invalid_CommentText_Then_Throw_ArgumentException(string? commentText)
    {
        // Arrange
        var comment = new FakeComment();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => comment.SetCommentText(commentText!));
    }

    #endregion CommentText Tests

    #endregion Tests

    #region MemberData Methods

    public static IEnumerable<object[]> ValidCreationData()
    {
        yield return [Guid.NewGuid(), "ValidCommentText"];
        yield return [Guid.NewGuid(), "ValidCommentText"];
    }

    #endregion MemberData Methods
}