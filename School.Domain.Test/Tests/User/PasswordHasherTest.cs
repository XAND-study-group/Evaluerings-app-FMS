using Moq;
using School.Domain.DomainServices;
using School.Domain.Test.Fakes.User;
using Xunit;
using Assert = Xunit.Assert;

namespace School.Domain.Test.Tests.User;

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
        var user = new FakeUser();
        user.ChangePassword(password);
        var wrongPassword = password + "wrong";

        // Act
        var result = user.PasswordHash.Verify(wrongPassword);

        // Assert
        Assert.False(result);
    }
    
    [Theory]
    [InlineData("Password123.")]
    [InlineData("ThisIsAVeryGoodPassword1.")]
    [InlineData("nouppercase1$D")]
    [InlineData("NoNumber!2£%")]
    [InlineData("NoSpecialChar1#")]
    public void Verify_ShouldReturnTrue_WhenPasswordMatchesHash(string password)
    {
        // Arrange
        var user = new FakeUser();
        user.ChangePassword(password);

        // Act
        var result = user.PasswordHash.Verify(password);

        // Assert
        Assert.True(result);
    }
}