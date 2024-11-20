using Module.Feedback.Domain.Test.Fakes;
using Moq;
using SharedKernel.Enums.Features.Vote;
using SharedKernel.Interfaces.DomainServices;
using SharedKernel.Interfaces.DomainServices.Interfaces;

namespace Module.Feedback.Domain.Test;

public class VoteTests
{
    #region Tests
    
    #region Creational Tests

    [Theory]
    [MemberData(nameof(ValidCreationData))]
    public void Given_Valid_Data_Then_Create_Success(Guid userId, VoteScale voteScale)
    {
        // Arrange
        var mockIHashIdService = new Mock<IHashIdService>();
        mockIHashIdService.Setup(hashId => hashId.Hash(userId)).Returns("ValidIdHash");
        
        // Act
        var vote = Vote.Create(userId, voteScale, mockIHashIdService.Object);
        
        // Assert
        Assert.NotNull(vote);
    }
    #endregion Creational Tests
    
    #region Update Tests

    [Theory]
    [MemberData(nameof(ValidUpdateData))]
    public void Given_Valid_Data_Then_Update_Success(FakeVote vote, VoteScale voteScale)
    {
        // Act
        vote.Update(voteScale);
        
        // Assert
        Assert.Equal(voteScale, vote.VoteScale);
    }
    #endregion Update Tests
    
    #endregion Tests
    
    #region MemberData Methods

    public static IEnumerable<object[]> ValidCreationData()
    {
        yield return [Guid.NewGuid(), VoteScale.UpVote];
        yield return [Guid.NewGuid(), VoteScale.DownVote];
    }
    
    public static IEnumerable<object[]> ValidUpdateData()
    {
        yield return [new FakeVote(VoteScale.DownVote), VoteScale.UpVote];
        yield return [new FakeVote(VoteScale.UpVote), VoteScale.DownVote];
    }
    #endregion MemberData Methods
}