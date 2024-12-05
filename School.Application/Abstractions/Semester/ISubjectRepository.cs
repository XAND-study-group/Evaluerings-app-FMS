namespace School.Application.Abstractions.Semester;

public interface ISubjectRepository
{
    Task<bool> DoesUserHaveSubject(Guid subjectId, Guid userId);
}