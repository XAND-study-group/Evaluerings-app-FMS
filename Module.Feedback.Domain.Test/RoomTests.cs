using Module.Feedback.Domain.Test.Fakes;

namespace Module.Feedback.Domain.Test;

public class RoomTests
{
    #region Tests
    
    #region Creational Tests
    #endregion Creational Tests
    
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
        Assert.Throws<ArgumentException>(() => room.SetTitle(new string('x',101)));
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
    
    #endregion Tests
}