using Module.Feedback.Domain.WrapperObjects;
using SharedKernel.Models;
using SharedKernel.ValueObjects;

namespace Module.Feedback.Domain.Entities;

public class Room : Entity
{
    public Title Title { get; protected set; }
    public Text Description { get; protected set; }

    private readonly List<ClassId> _classIds = [];
    public IReadOnlyCollection<ClassId> ClassIds => _classIds;

    private readonly List<NotificationUserId> _notificationSubscribedUserIds = [];
    public IReadOnlyCollection<NotificationUserId> NotificationSubscribedUserIds => _notificationSubscribedUserIds;

    private readonly List<Feedback> _feedbacks = [];
    public IReadOnlyCollection<Feedback> Feedbacks => _feedbacks;
    
    protected Room()
    {
    }

    private Room(string title, string description)
    {
        Title = title;
        Description = description;
    }
    
    #region Room Methods

    public static Room Create(string title, string description) => new(title, description);

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
        var classIdToAdd = ClassId.Create(classId);
        _classIds.Add(classIdToAdd);
    }

    public void RemoveClassId(Guid classId)
    {
        AssureClassIdIsInList(classId, _classIds);
        var classIdToRemove = _classIds.Single(c => c.ClassIdValue == classId);
        _classIds.Remove(classIdToRemove);
    }

    public void AddUserIdToNotificationList(Guid userId)
    {
        AssureNoDuplicateUserIds(userId, _notificationSubscribedUserIds);
        var notificationUserId = NotificationUserId.Create(userId);
        _notificationSubscribedUserIds.Add(notificationUserId);
    }

    public void RemoveUserIdFromNotificationList(Guid userId)
    {
        AssureUserIdIsInList(userId, _notificationSubscribedUserIds);
        var notificationUserId = _notificationSubscribedUserIds.Single(u => u.UserIdValue == userId);
        _notificationSubscribedUserIds.Remove(notificationUserId);
    }

    public void AddFeedback(Feedback feedback)
    {
        _feedbacks.Add(feedback);
    }

    #endregion Relational Methods

    #region Relational Business Logic Methods

    protected void AssureNoDuplicateClassIds(Guid classId, IEnumerable<ClassId> currentClassIds)
    {
        if (currentClassIds.Any(cId => cId.ClassIdValue == classId))
            throw new ArgumentException("Klasse er allerede tilføjet til forum");
    }

    protected void AssureClassIdIsInList(Guid classId, IEnumerable<ClassId> currentClassIds)
    {
        if (currentClassIds.All(cId => cId.ClassIdValue != classId))
            throw new ArgumentException("Klasse kunne ikke findes i rummet");
    }

    protected void AssureNoDuplicateUserIds(Guid userId, IEnumerable<NotificationUserId> currentUserIds)
    {
        if (currentUserIds.Any(cId => cId.UserIdValue == userId))
            throw new ArgumentException("Bruger er allerede tilføjet til notifikations listen");
    }

    protected void AssureUserIdIsInList(Guid userId, IEnumerable<NotificationUserId> currentUserIds)
    {
        if (currentUserIds.All(cId => cId.UserIdValue != userId))
            throw new ArgumentException("Bruger kunne ikke findes i notofikations listen");
    }

    #endregion Relational Business Logic Methods
}