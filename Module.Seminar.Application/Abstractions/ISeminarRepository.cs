namespace Module.Seminar.Application.Abstractions;

public interface ISeminarRepository
{
    #region Seminar
    Task CreateSeminarAsync(Domain.Entity.Seminar seminar);
    Task<IEnumerable<Domain.Entity.Seminar>> GetAllSeminars();
    #endregion

}