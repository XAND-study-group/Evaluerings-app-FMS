using Module.Feedback.Domain.DomainServices.Interfaces;
using Module.Feedback.Domain.Test.Fakes;
using Moq;
using SharedKernel.Dto.Features.Evaluering.Proxy;

namespace Module.Feedback.Domain.Test;

public class FeedbackTests
{
    #region Tests

    #region Creational Tests

    [Theory]
    [InlineData("5fb2d7bf-8c68-4da1-ad7e-613edabc3493", "ValidTitle", "ValidProblem", "ValidSolution")]
    public async Task Given_Valid_Data_Then_Create_Success(string userId, string title, string problem, string solution)
    {
        // Arrange
        var guid = Guid.Parse(userId);
        var room = new FakeRoom();

        var mockFeedbackService = new Mock<IValidationServiceProxy>();
        mockFeedbackService.Setup(x => x.IsAcceptableContentAsync(title, problem, solution))
            .ReturnsAsync(new GeminiResponse(true, ""));

        // Act
        var feedback =
            await Entities.Feedback.CreateAsync(guid, title, problem, solution, room, mockFeedbackService.Object);

        // Assert
        Assert.NotNull(feedback);
    }

    #endregion Creational Tests

    #region Title Tests

    [Fact]
    public void Given_Null_Title_Then_Throw_ArgumentException()
    {
        // Arrange
        var feedback = new FakeFeedback();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => feedback.SetTitle(null!));
    }

    [Fact]
    public void Given_Empty_Title_Then_Throw_ArgumentException()
    {
        // Arrange
        var feedback = new FakeFeedback();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => feedback.SetTitle(string.Empty));
    }

    [Fact]
    public void Given_WhiteSpace_Title_Then_Throw_ArgumentException()
    {
        // Arrange
        var feedback = new FakeFeedback();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => feedback.SetTitle(" "));
    }

    [Fact]
    public void Given_Valid_Title_Then_Void()
    {
        // Arrange
        var feedback = new FakeFeedback();

        // Act
        feedback.SetTitle("ValidTitle");
    }

    #endregion Title Tests

    #region Problem & Solution Tests

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Given_Invalid_Problem_Then_Throw_ArgumentException(string? problem)
    {
        // Arrange
        var feedback = new FakeFeedback();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => feedback.SetProblem(problem!));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Given_Invalid_Solution_Then_Throw_ArgumentException(string? solution)
    {
        // Arrange
        var feedback = new FakeFeedback();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => feedback.SetSolution(solution!));
    }

    [Fact]
    public void Given_Valid_Problem_And_Solution_Then_Void()
    {
        // Arrange
        var feedback = new FakeFeedback();

        // Act
        feedback.SetProblem("ValidProblem");
        feedback.SetSolution("ValidSolution");
    }

    #endregion Problem & Solution Tests

    #endregion Tests
}