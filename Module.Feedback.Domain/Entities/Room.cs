using SharedKernel.ValueObjects;

namespace Module.Feedback.Domain.Entities;

public class Room : Entity
{
    #region Properties

    public Title Title { get; protected set; }
    public Text Description { get; protected set; }

    private readonly List<Guid> _classIds = [];
    public IReadOnlyCollection<Guid> ClassIds => _classIds;

    private readonly List<Guid> _notificationSubscribedUserIds = [];
    public IReadOnlyCollection<Guid> NotificationSubscribedUserIds => _notificationSubscribedUserIds;

    private readonly List<Feedback> _feedbacks = [];
    public IReadOnlyCollection<Feedback> Feedbacks => _feedbacks;

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
    private Room(string title, string description, IEnumerable<Feedback> feedbacks)
    {
        Title = title;
        Description = description;
        _feedbacks = feedbacks.ToList();
    }
    #endregion Constructors

    #region Room Methods

    public static Room Create(string title, string description)
    {
        return new Room(title, description);
    }
    public static Room Create(string title, string description, IEnumerable<Feedback> feedbacks)
    {
        return new Room(title, description, feedbacks);
    }

    public void Update(string title, string description)
    {
        Title = title;
        Description = description;
    }

    #endregion Room Methods

    #region Relational Methods

    public void AddClassId(Guid classId)
    {
        AssureNoDuplicateClassIds(classId, _classIds);
        _classIds.Add(classId);
    }

    public void RemoveClassId(Guid classId)
    {
        AssureClassIdIsInList(classId, _classIds);
        _classIds.Remove(classId);
    }

    public void AddUserIdToNotificationList(Guid userId)
    {
        AssureNoDuplicateUserIds(userId, _notificationSubscribedUserIds);
        _notificationSubscribedUserIds.Add(userId);
    }

    public void RemoveUserIdFromNotificationList(Guid userId)
    {
        AssureUserIdIsInList(userId, _notificationSubscribedUserIds);
        _notificationSubscribedUserIds.Remove(userId);
    }

    public void AddFeedback(Feedback feedback)
    {
        _feedbacks.Add(feedback);
    }

    #endregion Relational Methods

    #region Relational Business Logic Methods

    protected void AssureNoDuplicateClassIds(Guid classId, IEnumerable<Guid> currentClassIds)
    {
        if (currentClassIds.Any(cId => cId == classId))
            throw new ArgumentException("Klasse er allerede tilføjet til forum");
    }

    protected void AssureClassIdIsInList(Guid classId, IEnumerable<Guid> currentClassIds)
    {
        if (currentClassIds.All(cId => cId != classId))
            throw new ArgumentException("Klasse kunne ikke findes i rummet");
    }

    protected void AssureNoDuplicateUserIds(Guid userId, IEnumerable<Guid> currentUserIds)
    {
        if (currentUserIds.Any(cId => cId == userId))
            throw new ArgumentException("Bruger er allerede tilføjet til notifikations listen");
    }

    protected void AssureUserIdIsInList(Guid userId, IEnumerable<Guid> currentUserIds)
    {
        if (currentUserIds.All(cId => cId != userId))
            throw new ArgumentException("Bruger kunne ikke findes i notofikations listen");
    }

    #endregion Relational Business Logic Methods
}