namespace Module.Seminar.Application.Abstractions;

public interface ISeminarRepository
{
    #region Seminar
    Task CreateSeminarAsync(Domain.Entity.Seminar seminar);
    Task<IEnumerable<Domain.Entity.Seminar>> GetAllSeminarsAsync();
    Task<Domain.Entity.Seminar> GetSeminarByIdAsync(Guid seminarId);
    Task<User> GetStudentByIdAsync(Guid studentId);
    Task AddStudentToSeminarAsync(Domain.Entity.Seminar seminar);
    #endregion
}