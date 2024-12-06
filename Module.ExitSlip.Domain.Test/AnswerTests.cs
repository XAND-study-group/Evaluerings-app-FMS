using Module.ExitSlip.Domain.Entities;
using Module.ExitSlip.Domain.Test.Fakes;
using Moq;
using Xunit;

namespace Module.ExitSlip.Domain.Test;

public class AnswerTests
{
    #region Tests

    #region Command Tests

    [Theory]
    [MemberData(nameof(Valid_Create_Data))]
    public void Given_Valid_Data_Then_Create_Success(string text)
    {
        // Act
        var answer = Answer.Create(text, It.IsAny<Guid>());

        // Assert
        Assert.NotNull(answer);
    }

    [Theory]
    [MemberData(nameof(Valid_Update_TestData))]
    public void Given_Valid_Update_Data_Then_Update_Success(string newText, FakeAnswer answer)
    {
        // Act
        answer.UpdateAnswer(newText);

        // Assert
        Assert.Equal(newText, answer.Text);
    }

    #endregion Command Tests

    #region Text Tests

    [Theory]
    [MemberData(nameof(Invalid_Text_Data))]
    public void Given_Invalid_Text_Data_Then_Throw_ArgumentException(string? text)
    {
        // Arrange
        var answer = new FakeAnswer();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => answer.SetText(text));
    }

    #endregion Text Tests

    #endregion Tests

    #region MemberData Methods

    public static IEnumerable<object[]> Valid_Create_Data()
    {
        yield return ["ValidText"];
        yield return ["V"];
        yield return [new string('x', 500)];
    }

    public static IEnumerable<object[]> Invalid_Text_Data()
    {
        yield return [" "];
        yield return [null!];
        yield return [string.Empty];
    }

    public static IEnumerable<object[]> Valid_Update_TestData()
    {
        yield return ["NewValidText", new FakeAnswer("ValidText")];
        yield return ["Q", new FakeAnswer("V")];
        yield return [new string('q', 500), new FakeAnswer(new string('x', 500))];
    }

    #endregion MemberData Methods
}