using Module.Feedback.Domain.Test.Fakes;

namespace Module.Feedback.Domain.Test;

public class RoomTests
{
    #region Tests

    #region Creational Tests

    [Fact]
    public void Given_Valid_Data_Then_Create_Success()
    {
        // Arrange
        var title = "ValidTitle";
        var description = "ValidDescription";

        // Act
        var room = Room.Create(title, description);

        // Assert
        Assert.NotNull(room);
    }

    #endregion Creational Tests

    #region Update Tests

    [Theory]
    [MemberData(nameof(ValidTitleUpdateData))]
    public void Given_Valid_Title_Then_Update_Success(FakeRoom roomToUpdate, string expectedTitle)
    {
        // Act
        roomToUpdate.Update(expectedTitle, roomToUpdate.Description);
        
        // Assert
        Assert.Equal(expectedTitle, roomToUpdate.Title);
    }
    
    [Theory]
    [MemberData(nameof(ValidDescriptionUpdateData))]
    public void Given_Valid_Description_Then_Update_Success(FakeRoom roomToUpdate, string expectedDescription)
    {
        // Act
        roomToUpdate.Update(roomToUpdate.Title, expectedDescription);
        
        // Assert
        Assert.Equal(expectedDescription, roomToUpdate.Description);
    }

    [Theory]
    [MemberData(nameof(InvalidUpdateData))]
    public void Given_Invalid_Data_Then_Update_Failure(FakeRoom roomToUpdate, string title, string description)
    {
        // Arrange
        var expectedTitle = roomToUpdate.Title;
        var expectedDescription = roomToUpdate.Description;
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => roomToUpdate.Update(title, description));
        Assert.Equal(roomToUpdate.Title, expectedTitle);
        Assert.Equal(roomToUpdate.Description, expectedDescription);
    }

    #endregion Update Tests

    #region Title Tests

    [Fact]
    public void Given_Valid_Title_Then_Void()
    {
        // Arrange
        var room = new FakeRoom();

        // Act
        room.SetTitle("ValidTitle");
    }

    [Fact]
    public void Given_WhiteSpace_Title_Then_Throw_ArgumentException()
    {
        // Arrange
        var room = new FakeRoom();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => room.SetTitle(" "));
    }

    [Fact]
    public void Given_Null_Title_Then_Throw_ArgumentException()
    {
        // Arrange
        var room = new FakeRoom();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => room.SetTitle(null!));
    }

    [Fact]
    public void Given_Empty_Title_Then_Throw_ArgumentException()
    {
        // Arrange
        var room = new FakeRoom();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => room.SetTitle(string.Empty));
    }

    [Fact]
    public void Given_String_Length_Bigger_Than_OneHundred_Then_Throw_ArgumentException()
    {
        // Arrange
        var room = new FakeRoom();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => room.SetTitle(new string('x', 101)));
    }

    #endregion Title Tests

    #region Description Tests

    [Fact]
    public void Given_Valid_Description_Then_Void()
    {
        // Arrange
        var room = new FakeRoom();

        // Act
        room.SetDescription("ValidDescription");
    }

    [Fact]
    public void Given_WhiteSpace_Description_Then_Throw_ArgumentException()
    {
        // Arrange
        var room = new FakeRoom();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => room.SetDescription(" "));
    }

    [Fact]
    public void Given_Null_Description_Then_Throw_ArgumentException()
    {
        // Arrange
        var room = new FakeRoom();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => room.SetDescription(null!));
    }

    [Fact]
    public void Given_Empty_Description_Then_Throw_ArgumentException()
    {
        // Arrange
        var room = new FakeRoom();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => room.SetDescription(string.Empty));
    }

    [Fact]
    public void Given_String_Length_Bigger_Than_FiveHundred_Then_Throw_ArgumentException()
    {
        // Arrange
        var room = new FakeRoom();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => room.SetDescription(new string('x', 501)));
    }

    #endregion Description Tests
    
    #region AddFeedback Tests

    [Theory]
    [MemberData(nameof(ValidFeedbackData))]
    public void Given_Valid_Feedback_Then_List_Count_Increased(Guid userId, string problem, string solution)
    {
        // Arrange
        var room = new FakeRoom();
        var expectedCount = 1;
        
        // Act
        room.AddFeedback(userId, problem, solution);
        
        // Assert
        Assert.Equal(expectedCount, room.Feedbacks.Count);
    }
    #endregion AddFeedback Tests

    #endregion Tests

    #region MemberData Methods

    public static IEnumerable<object[]> ValidTitleUpdateData()
    {
        yield return new object[]
        {
            new FakeRoom("ValidTitle", "ValidDescription"),
            "AnotherValidTitle"
        };
    }
    
    public static IEnumerable<object[]> ValidDescriptionUpdateData()
    {
        yield return new object[]
        {
            new FakeRoom("ValidTitle", "ValidDescription"),
            "AnotherValidDescription"
        };
    }
    
    public static IEnumerable<object[]> InvalidUpdateData()
    {
        yield return new object[]
        {
            new FakeRoom("ValidTitle", "ValidDescription"),
            " ",
            "ValidDescription"
        };
        
        yield return new object[]
        {
            new FakeRoom("ValidTitle", "ValidDescription"),
            "ValidTitle",
            " "
        };
    }

    public static IEnumerable<object[]> ValidFeedbackData()
    {
        yield return new object[]
        {
            Guid.NewGuid(),
            "ValidProblem",
            "ValidSolution"
        };
    }

    #endregion MemberData Methods
}