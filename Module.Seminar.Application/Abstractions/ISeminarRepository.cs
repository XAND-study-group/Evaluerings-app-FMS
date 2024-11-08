using Module.Seminar.Domain.Entity;

namespace Module.Seminar.Application.Abstractions;

public interface ISeminarRepository
{
    #region Seminar
    Task CreateSeminarAsync(Domain.Entity.Seminar seminar);
    Task<IEnumerable<Domain.Entity.Seminar>> GetAllSeminarsAsync();
    Task<Domain.Entity.Seminar> GetSeminarByIdAsync(Guid seminarId);
    Task<User> GetUserByIdAsync(Guid studentId);
    Task AddUserToSeminarAsync(Domain.Entity.Seminar seminar);
    #endregion
}