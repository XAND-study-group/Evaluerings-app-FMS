using SharedKernel.ValueObjects;

namespace Module.Feedback.Domain;

public class Room : Entity
{
    #region Properties

    public Title Title { get; protected set; }
    public Text Description { get; protected set; }
    private readonly List<Guid> _classIds = [];
    public IReadOnlyCollection<Guid> ClassIds => _classIds;

    #endregion Properties

    #region Constructors

    protected Room()
    {
    }

    private Room(string title, string description)
    {
        Title = title;
        Description = description;
    }

    #endregion Constructors

    #region Room Methods

    public static Room Create(string title, string description)
        => new Room(title, description);

    public void Update(string title, string description)
    {
        Title = title;
        Description = description;
    }

    #endregion Room Methods

    #region Relational Methods

    public void AddClassIdAsync(Guid classId)
    {
        AssureNoDuplicateClassIds(classId, _classIds);
        _classIds.Add(classId);
    }

    #endregion Relational Methods

    #region Relational Business Logic Methods

    protected void AssureNoDuplicateClassIds(Guid classId, IEnumerable<Guid> currentClassIds)
    {
        if (currentClassIds.Any(cId => cId == classId))
            throw new ArgumentException("Klasse er allerede tilføjet til forum");
    }

    #endregion Relational Business Logic Methods
}