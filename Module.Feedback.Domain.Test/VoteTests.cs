using Module.Feedback.Domain.Entities;
using Module.Feedback.Domain.Test.Fakes;
using SharedKernel.Enums.Features.Vote;
using Xunit;

namespace Module.Feedback.Domain.Test;

public class VoteTests
{
    #region Tests

    #region Creational Tests

    [Theory]
    [MemberData(nameof(ValidCreationData))]
    public void Given_Valid_Data_Then_Create_Success(Guid userId, VoteScale voteScale)
    {
        // Act
        var vote = Vote.Create(userId, voteScale);

        // Assert
        Assert.NotNull(vote);
    }

    #endregion Creational Tests

    #region Update Tests

    [Theory]
    [MemberData(nameof(ValidUpdateData))]
    public void Given_Valid_Data_Then_Update_Success(FakeVote vote, Guid userId, VoteScale voteScale)
    {
        // Act
        vote.Update(userId, voteScale);

        // Assert
        Assert.Equal(voteScale, vote.VoteScale);
    }

    #endregion Update Tests

    #endregion Tests

    #region MemberData Methods

    public static IEnumerable<object[]> ValidCreationData()
    {
        yield return [Guid.NewGuid(), VoteScale.Up];
        yield return [Guid.NewGuid(), VoteScale.Down];
    }

    public static IEnumerable<object[]> ValidUpdateData()
    {
        var userId = Guid.Parse("4cf1ab55-5356-437b-a578-77812884c146");
        yield return
            [new FakeVote(userId, VoteScale.Down), userId, VoteScale.Up];
        yield return
            [new FakeVote(userId, VoteScale.Up), userId, VoteScale.Down];
    }

    #endregion MemberData Methods
}