using Module.Feedback.Domain;

namespace Module.Feedback.Application.Abstractions;

public interface IVoteRepository
{
    Task<Room> GetRoomByIdAsync(Guid roomId);
    Task<Vote> GetVoteByIdAsync(Guid voteId);
    Task CreateVoteAsync(Vote vote);
    Task UpdateVoteAsync(Vote vote, byte[] rowVersion);
    Task DeleteVoteAsync(Vote vote, byte[] rowVersion);
}