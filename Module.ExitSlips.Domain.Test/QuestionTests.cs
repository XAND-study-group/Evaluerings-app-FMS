using Module.ExitSlip.Domain.Entities;
using Module.ExitSlip.Domain.Test.Fakes;
using Xunit;

namespace Module.ExitSlip.Domain.Test;

public class QuestionTests
{
    #region Tests

    #region Command Tests

    [Theory]
    [MemberData(nameof(Valid_Create_Data))]
    public void Given_Valid_Data_Then_Create_Success(string text)
    {
        // Act
        var question = Question.Create(text);

        // Assert
        Assert.NotNull(question);
    }

    [Theory]
    [MemberData(nameof(Valid_Update_Data))]
    public void Given_Valid_Update_Data_Then_Update_Success(string newText, FakeQuestion question)
    {
        // Act
        question.UpdateQuestion(newText);
        
        // Assert
        Assert.Equal(newText, question.Text);
    }

    #endregion Command Tests
    
    #region Text Tests

    [Theory]
    [MemberData(nameof(Invalid_Text_Data))]
    public void Given_Invalid_Text_Data_Then_Throw_ArgumentException(string text)
    {
        // Arrange
        var question = new FakeQuestion();
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => question.SetText(text));
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

    public static IEnumerable<object[]> Valid_Update_Data()
    {
        yield return ["NewValidText", new FakeQuestion("ValidText")];
        yield return ["Q", new FakeQuestion("V")];
        yield return [new string('q', 500), new FakeQuestion(new string('x', 500))];
    }

    #endregion MemberData Methods
}