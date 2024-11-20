namespace Module.Semester.Domain.Entities;

public class User : Entity
{
    public string Firstname { get; protected set; }
    public string Lastname { get; protected set; }
    public string Email { get; protected set; }
    private readonly List<Semester> _semesters = [];
    public IReadOnlyCollection<Semester> Semesters => _semesters;
}