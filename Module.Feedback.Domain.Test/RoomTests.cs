using Module.Feedback.Domain.DomainServices.Interfaces;
using Module.Feedback.Domain.Test.Fakes;
using Moq;
using SharedKernel.Dto.Features.Evaluering.Proxy;

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
    
    [Theory]
    [MemberData(nameof(ValidStringLengths))]
    public void Given_Valid_String_Length_Then_Void(string title)
    {
        // Arrange
        var room = new FakeRoom();

        // Act
        room.SetTitle(title);
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
    
    #region AddClass Tests

    [Theory]
    [MemberData(nameof(InvalidAddClassData))]
    public void Given_Duplicate_ClassIds_Then_Throw_ArgumentException(Guid classId, IEnumerable<Guid> currentClassIds)
    {
        // Arrange
        var room = new FakeRoom();
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => room.AssureNoDuplicateClassIds(classId, currentClassIds));
    }
    
    [Theory]
    [MemberData(nameof(ValidAddClassData))]
    public void Given_Unique_ClassIds_Then_Void(Guid classId, IEnumerable<Guid> currentClassIds)
    {
        // Arrange
        var room = new FakeRoom();
        
        // Act
        room.AssureNoDuplicateClassIds(classId, currentClassIds);
    }
    #endregion AddClass Tests

    #endregion Tests

    #region MemberData Methods

    public static IEnumerable<object[]> ValidTitleUpdateData()
    {
        yield return
        [
            new FakeRoom("ValidTitle", "ValidDescription"),
            "AnotherValidTitle"
        ];
    }

    public static IEnumerable<object[]> ValidDescriptionUpdateData()
    {
        yield return
        [
            new FakeRoom("ValidTitle", "ValidDescription"),
            "AnotherValidDescription"
        ];
    }

    public static IEnumerable<object[]> InvalidUpdateData()
    {
        yield return
        [
            new FakeRoom("ValidTitle", "ValidDescription"),
            " ",
            "ValidDescription"
        ];

        yield return
        [
            new FakeRoom("ValidTitle", "ValidDescription"),
            "ValidTitle",
            " "
        ];
    }

    public static IEnumerable<object[]> ValidStringLengths()
    {
        yield return [new string('x', 100)];
        yield return [new string('x', 50)];
        yield return [new string('x', 1)];
    }

    public static IEnumerable<object[]> InvalidAddClassData()
    {
        var currentClassIds = GetCurrentClassIds();
        yield return
        [
            Guid.Parse("447155e9-800c-4230-a7fe-ec1bd2f213a7"),
            currentClassIds
        ];
    }
    
    public static IEnumerable<object[]> ValidAddClassData()
    {
        var currentClassIds = GetCurrentClassIds();
        yield return
        [
            Guid.NewGuid(),
            currentClassIds
        ];
    }

    private static IEnumerable<Guid> GetCurrentClassIds()
    {
        return new[]
        {
            Guid.Parse("7b365a52-211f-48d2-8c5d-c7694f78f86f"),
            Guid.Parse("447155e9-800c-4230-a7fe-ec1bd2f213a7")
        };
    }

    #endregion MemberData Methods
}