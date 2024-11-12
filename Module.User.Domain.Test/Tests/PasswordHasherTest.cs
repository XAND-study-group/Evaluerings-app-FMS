using Module.User.Domain.DomainServices;
using Moq;
using Xunit;

namespace Module.User.Domain.Test.Tests;

public class PasswordHasherTest
{
    [Theory]
    [MemberData(nameof(ValidPasswords))]
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
    [MemberData(nameof(ValidPasswords))]
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

    #region MemberData
    
    public static IEnumerable<object[]> ValidPasswords()
    {
        yield return new object[] { "ValidPassword123!" };
        yield return new object[] { "AaaaaaaBbbbbbb123..." };
        yield return new object[] { "SomeWeirdPassword45#!" };
        yield return new object[] { "BestPassword4*" };
    }

    public static IEnumerable<object[]> InvalidPasswords()
    {
        yield return new object[] { "" };
        yield return new object[] { "short" };
        yield return new object[] { "nouppercase1!" };
        yield return new object[] { "NoNumber!" };
        yield return new object[] { "NoSpecialChar1" };
    }

    #endregion
}