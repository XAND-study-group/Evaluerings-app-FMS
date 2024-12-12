using SharedKernel.Models;
using SharedKernel.ValueObjects;

namespace Module.ExitSlip.Domain.Entities;

public class Answer : Entity
{
    public Guid UserId { get; }
    public Text Text { get; protected set; }
    
    protected Answer()
    {
    }

    private Answer(string text, Guid userId)
    {
        Text = text;
        UserId = userId;
    }
    
    #region Methods

    internal static Answer Create(string text, Guid userId) => new(text, userId);

    internal void UpdateAnswer(string newText)
    {
        Text = newText;
    }

    #endregion
}