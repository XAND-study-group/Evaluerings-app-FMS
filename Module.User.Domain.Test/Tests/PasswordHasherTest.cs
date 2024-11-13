using Module.User.Domain.DomainServices;
using Moq;
using Xunit;

namespace Module.User.Domain.Test.Tests;

public class PasswordHasherTest
{
    [Theory]
    [InlineData("ValidPassword123!")]
    [InlineData("AaaaaaaBbbbbbb123...")]
    [InlineData("SomeWeirdPassword45#!")]
    [InlineData("BestPassword4*")]
    public void Verify_ShouldReturnFalse_WhenPasswordDoesNotMatchHash(string password)
    {
        // Arrange
        var mockHasher = new Mock<PasswordHasher>
        {
            CallBase = true
        };
        var hash = mockHasher.Object.Hash(password);
        var wrongPassword = password + "wrong";

        // Act
        var result = mockHasher.Object.Verify(wrongPassword, hash);

        // Assert
        Assert.False(result);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("short")]
    [InlineData("nouppercase1!")]
    [InlineData("NoNumber!")]
    [InlineData("NoSpecialChar1")]
    public void Verify_ShouldReturnTrue_WhenPasswordMatchesHash(string password)
    {
        // Arrange
        var mockHasher = new Mock<PasswordHasher>
        {
            CallBase = true
        };
        
        var hash = mockHasher.Object.Hash(password);

        // Act
        var result = mockHasher.Object.Verify(password, hash);

        // Assert
        Assert.True(result);
    }
}